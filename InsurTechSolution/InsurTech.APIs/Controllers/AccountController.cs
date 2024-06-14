using Azure;
using InsurTech.APIs.DTOs;
using InsurTech.APIs.Errors;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
