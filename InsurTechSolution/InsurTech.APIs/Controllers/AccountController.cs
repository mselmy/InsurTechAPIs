using InsurTech.APIs.DTOs;
using InsurTech.APIs.DTOs.Company;
using InsurTech.APIs.DTOs.Customer;
using InsurTech.APIs.Errors;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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
				IsApprove = model.Status,
				TaxNumber = model.TaxNumber,
				Location = model.Location,
				UserType = UserType.Company
			};

			var Result = await _userManager.CreateAsync(User, model.Password);

			if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Error in creating user"));

			return Ok(new UserDTO()
            {
				Email = User.Email,
				Name = User.UserName,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			});
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
				BirthDate = model.BirthDate,
				UserType = UserType.Customer
			};

			var Result = await _userManager.CreateAsync(User, model.Password);

			if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Error in creating user"));

			return Ok(new UserDTO()
            {
				Email = User.Email,
				Name = User.UserName,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			});
		}

		#endregion
/*
		#region RegisterAdmin

		[HttpPost("RegisterAdmin")]
		public async Task<ActionResult<UserDTO>> RegisterAdmin(RegisterAdminInput model)
		{
			if (await _userManager.FindByEmailAsync(model.email))
			{
				if (await _userManager.FindByEmailAsync(model.EmailAddress) != null) return BadRequest(new ApiResponse(400, "Email is already taken"));
				if (await _userManager.FindByNameAsync(model.UserName) != null) return BadRequest(new ApiResponse(400, "UserName is already taken"));

				var User = new AppUser
				{
					Email = model.EmailAddress,
					UserName = model.UserName,
					Name = model.Name,
					PhoneNumber = model.phoneNumber,
					IsApprove = IsApprove.approved,
					UserType = UserType.Admin
				};

				var Result = await _userManager.CreateAsync(User, model.Password);

				if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Error in creating user"));

				return Ok(new UserDTO()
				{
					Email = User.Email,
					Name = User.UserName,
					Token = await _tokenService.CreateTokenAsync(User, _userManager)
				});
			}
			else
			{
				return BadRequest(new ApiResponse(400, "You are not authorized to create an admin"));
			}

		}

		#endregion*/



	}
}
