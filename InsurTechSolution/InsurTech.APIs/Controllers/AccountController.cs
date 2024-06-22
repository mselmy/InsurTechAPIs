using InsurTech.APIs.DTOs;
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
using Microsoft.Identity.Client.AppConfig;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService, IEmailService emailService,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        #region Login

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
            if (!(User.IsApprove == IsApprove.approved)) return Unauthorized(new ApiResponse(401));


            return Ok(new UserDTO()
            {
                Email = User.Email,
                Name = User.UserName,
                Token = await _tokenService.CreateTokenAsync(User, _userManager),
                Id=User.Id,
                UserType=User.UserType
            }); 

        }

        #endregion

        #region RegisterCompany

        [HttpPost("RegisterCompany")]
        public async Task<ActionResult<ApiResponse>> RegisterCompany(RegisterCompanyInput model)
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

           await _userManager.AddToRoleAsync(User, Roles.Company);

            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);

            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = User.Email }, Request.Scheme);

            if (confirmationLink is null) return BadRequest(new ApiResponse(400, "Error in sending confirmation email"));

            await _emailService.SendConfirmationEmail(User.Email, confirmationLink);

            // Send notification to admin
            var notification = new Notification
            {
                Body = $"A new company {User.Name} has registered and needs approval.",
                UserId = "1" ,
                IsRead = false
            };
            await _unitOfWork.Repository<Notification>().AddAsync(notification);
            await _unitOfWork.CompleteAsync();


            return Ok( new ApiResponse(200, $"Company Registered Successfully, Please check your email to confirm your account {User.Email}"));
        }

        #endregion

        #region RegisterCustomer

        [HttpPost("RegisterCustomer")]
        public async Task<ActionResult<ApiResponse>> RegisterCustomer(RegisterCustomerInput model)
        {
            if (await _userManager.FindByEmailAsync(model.EmailAddress) != null) return BadRequest(new ApiResponse(400, "Email is already taken"));
            if (await _userManager.FindByNameAsync(model.UserName) != null) return BadRequest(new ApiResponse(400, "UserName is already taken"));

            var User = new Customer
            {
                Email = model.EmailAddress,
                UserName = model.UserName,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                IsApprove = IsApprove.approved,
                NationalID = model.NationalId,
                BirthDate = DateOnly.Parse("1999-07-22"),
                //BirthDate = new DateTime(),
                UserType = UserType.Customer
            };

            var Result = await _userManager.CreateAsync(User, model.Password);

            if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Error in creating user"));

            await _userManager.AddToRoleAsync(User, Roles.Customer);


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);

            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = User.Email }, Request.Scheme);

            if (confirmationLink is null) return BadRequest(new ApiResponse(400, "Error in sending confirmation email"));

            await _emailService.SendConfirmationEmail(User.Email, confirmationLink);

            var notification = new Notification
            {
                Body = $"A new customer {User.Name} has registered.",
                UserId = "1" ,
                IsRead=false
            };
            await _unitOfWork.Repository<Notification>().AddAsync(notification);
            await _unitOfWork.CompleteAsync();

            return Ok(new ApiResponse(200, $"Customer Registered Successfully, Please check your email to confirm your account {User.Email}"));
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

        #region resend confirmation Email
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
        [HttpPost("ForgotPasswordAngular")]
        public async Task<IActionResult> ForgotPasswordAngular(ForgotPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(404, "User not found"));
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string angularPort = "4200";
            string componentRoute = "/resetpassword";
            string angularBaseUrl = $"http://localhost:{angularPort}";

            string resetLink = $"{angularBaseUrl}{componentRoute}?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";
            await _emailService.SendPasswordResetEmail(model.Email, resetLink);

            return Ok(new { Message = "Password reset email sent." });
        }

        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string token, string email)
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


        #region Login By Google
        [HttpPost("GooglleLogin")]
        public async Task<IActionResult> GoogleLogin(Emailobj mail)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(mail.Email);

                if (user == null)
                {
                    var userNameNew = mail.Email.Split('@')[0];
                    user = new Customer
                    {
                        Name = new string(userNameNew.Where(c => char.IsLetter(c)).ToArray()),
                        UserName = new string(userNameNew.Where(c => char.IsLetter(c)).ToArray()),
                        Email = mail.Email,
                        PhoneNumber = "01556675022",
                        IsApprove = IsApprove.approved,
                        NationalID = "11111111111111",
                        UserType = UserType.Customer,

                    };

                    var result = await _userManager.CreateAsync(user, "Asmaa***12345");

                    if (!result.Succeeded)
                    {
                        return BadRequest("Failed to create user.");
                    }

                }

                var tokenString = await _tokenService.CreateTokenAsync(user, _userManager);

                var userDto = new UserDTO
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Token = tokenString,
                    Id = user.Id,
                    UserType = user.UserType
                };

                return Ok(userDto);
            }
            catch (InvalidJwtException ex)
            {
                return BadRequest("Invalid token.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
    public class Emailobj()
    {
        public string Email { get; set; }
    }

    #endregion
}

