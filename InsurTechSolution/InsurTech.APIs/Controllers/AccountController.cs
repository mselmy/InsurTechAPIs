﻿using InsurTech.APIs.DTOs;
using InsurTech.APIs.DTOs.Company;
using InsurTech.APIs.DTOs.Customer;
﻿using Azure;
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

        #region RegisterCompany

        [HttpPost("RegisterCompany")]
		public async Task<ActionResult<UserDTO>> RegisterCompany(RegisterCompanyInput model)
        {
			if (await _userManager.FindByEmailAsync(model.EmailAddress) != null) return BadRequest(new ApiResponse(400, "Email is already taken"));
			if (await _userManager.FindByNameAsync(model.UserName) != null) return BadRequest(new ApiResponse(400, "UserName is already taken"));

			var User = new Company
            {
				Email = model.EmailAddress,
				UserName = model.UserName,
				Name = model.Name,
				PhoneNumber = model.phoneNumber,
				IsApprove = IsApprove.pending,
				TaxNumber = model.TaxNumber,
				Location = model.Location,
				UserType = UserType.Company
			};

			var Result = await _userManager.CreateAsync(User, model.Password);

			if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Error in creating user"));


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);

            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = User.Email }, Request.Scheme);

            if (confirmationLink is null) return BadRequest(new ApiResponse(400, "Error in sending confirmation email"));

            await _emailService.SendConfirmationEmail(User.Email, confirmationLink);



			return Ok(
                    
                new
                {
                    Message = $"Company Registered Successfully, Please check your email to confirm your account {User.Email}",
                }
                );
		}

        #endregion

		#region ApproveCompany
		[HttpPost("ApproveCompany")]
		public async Task<ActionResult> ApproveCompany(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return NotFound(new ApiResponse(404, "User not found"));
			if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
			if(user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is already approved"));
			if(user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is rejected"));
			user.IsApprove = IsApprove.approved;
			await _userManager.UpdateAsync(user);
			return Ok();
		}
		#endregion

		#region RejectCompany
		[HttpPost("RejectCompany")]
		public async Task<ActionResult> RejectCompany(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return NotFound(new ApiResponse(404, "User not found"));
			if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
			if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is already rejected"));
			if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is approved"));
			user.IsApprove = IsApprove.rejected;
			await _userManager.UpdateAsync(user);
			return Ok();
		}
		#endregion

		#region RegisterCustomer

		[HttpPost("RegisterCustomer")]
		public async Task<ActionResult<UserDTO>> RegisterCustomer(RegisterCustomerInput model)
        {
			if (await _userManager.FindByEmailAsync(model.EmailAddress) != null) return BadRequest(new ApiResponse(400, "Email is already taken"));
			if (await _userManager.FindByNameAsync(model.UserName) != null) return BadRequest(new ApiResponse(400, "UserName is already taken"));

			var User = new Customer
            {
				Email = model.EmailAddress,
				UserName = model.UserName,
				Name = model.Name,
				PhoneNumber = model.PhoneNumber,
				IsApprove = IsApprove.pending,
				NationalID = model.NationalId,
				//BirthDate = model.BirthDate,
				UserType = UserType.Customer
			};

			var Result = await _userManager.CreateAsync(User, model.Password);

			if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Error in creating user"));



            var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);

            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = User.Email }, Request.Scheme);

            if (confirmationLink is null) return BadRequest(new ApiResponse(400, "Error in sending confirmation email"));

            await _emailService.SendConfirmationEmail(User.Email, confirmationLink);



            return Ok(

                new
                {
                    Message = $"Customer Registered Successfully, Please check your email to confirm your account {User.Email}",
                }
                );
        }

		#endregion

		#region ApproveCustomer
		[HttpPost("ApproveCustomer")]
		public async Task<ActionResult> ApproveCustomer(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return NotFound(new ApiResponse(404, "User not found"));
			if (user.UserType != UserType.Customer) return BadRequest(new ApiResponse(400, "User is not a customer"));
			if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is already approved"));
			if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is rejected"));
			user.IsApprove = IsApprove.approved;
			await _userManager.UpdateAsync(user);
			return Ok();
		}
		#endregion

		#region RejectCustomer
		[HttpPost("RejectCustomer")]
		public async Task<ActionResult> RejectCustomer(string id)
		{
			//check iff the current user is admin
			//if(User.FindFirst(ClaimTypes.NameIdentifier)?.Value != "admin") return Unauthorized(new ApiResponse(401, "Unauthorized")); 
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return NotFound(new ApiResponse(404, "User not found"));
			if (user.UserType != UserType.Customer) return BadRequest(new ApiResponse(400, "User is not a customer"));
			if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is already rejected"));
			if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is approved"));
			user.IsApprove = IsApprove.rejected;
			await _userManager.UpdateAsync(user);
			return Ok();
		}
		#endregion

		#region Logout
		[HttpPost("Logout")]
		public async Task<ActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok();
		}
        #endregion


        #region resend confirmation Emai
        [HttpPost("ResendConfirmationEmail")]

        public async Task<ActionResult> ResendConfirmationEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(404, "User not found"));
            }

            if (user.EmailConfirmed)
            {
                return BadRequest(new ApiResponse(400, "Email is already confirmed"));
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);
            await _emailService.SendConfirmationEmail(user.Email, confirmationLink);

            return Ok(new { Message = $"Confirmation email sent to your email  {user.Email}" });
        }

           
        #endregion

        #region Confirm Email
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(404, "User not found"));
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Error confirming email"));
            }

            return Ok(new { Message = "Email confirmed successfully." });
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
