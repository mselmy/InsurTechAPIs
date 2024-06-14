using Azure;
using InsurTech.APIs.DTOs;
using InsurTech.APIs.Errors;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Service;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Google.Apis.Auth;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        
        #region Login

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
            if (!(User.IsApprove==IsApprove.approved)) return Unauthorized(new ApiResponse(401));

            
            return Ok(new UserDTO()
            {
                Email = User.Email,
                Name = User.UserName,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            }); ;

        }

        #endregion

        #region Reset Password

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(404, "User not found"));
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
            await _emailService.SendPasswordResetEmail(model.Email, resetLink);

            return Ok(new { Message = "Password reset email sent." });
        }
        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string token  , string email)
        {
            return Ok(new
            {
                token,
                email
            });
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(404, "User not found"));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Error resetting password"));
            }

            return Ok(new { Message = "Password reset successful." });
        }




        #endregion

        #region  Login By Google In Api

        [HttpGet("LoginWithGoogle")]
        public IActionResult LoginWithGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Google authentication failed"));
            }

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (email == null)
            {
                return BadRequest(new ApiResponse(400, "Google authentication failed"));
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var userNameNew = email.Split('@')[0];
                user = new AppUser()
                {
                    UserType = UserType.Admin,
                    UserName = new string(userNameNew.Where(c => char.IsLetter(c)).ToArray()),
                    Email = email,
                    PhoneNumber = "01556675022",
                    IsApprove = IsApprove.approved
                };
                var resultt = await _userManager.CreateAsync(user, "Asmaa***12345");
                if (!resultt.Succeeded) return BadRequest(resultt.Errors);

            }

            var userDto = new UserDTO
            {
                Email = user.Email,
                Name = user.UserName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(userDto);
        }
        #endregion

        #region Login By Google Front

        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] string token)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            var info = new UserLoginInfo("Google", payload.Subject, "Google");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                var userNameNew = payload.Email.Split('@')[0];
                user = new AppUser()
                {
                    UserType = UserType.Customer,
                    UserName = new string(userNameNew.Where(c => char.IsLetter(c)).ToArray()),
                    Email = payload.Email,
                    PhoneNumber = "01556675022",
                    IsApprove = IsApprove.approved
                };
                var resultt = await _userManager.CreateAsync(user, "Asmaa***12345");
                await _userManager.AddLoginAsync(user, info);
            }

            var jwtToken = await _tokenService.CreateTokenAsync(user, _userManager);
            return Ok(new { Token = jwtToken });
        }

        #endregion
    }
}
