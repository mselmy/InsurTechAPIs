using AutoMapper;
using InsurTech.APIs.DTOs.Company;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(UserManager<AppUser> userManager, IEmailService emailService, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _emailService = emailService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        #region ApproveCompany
        [HttpPost("ApproveCompany/{id}")]
        public async Task<ActionResult> ApproveCompany([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
            //if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is already approved"));
            //if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is rejected"));
            user.IsApprove = IsApprove.approved;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        #endregion

        #region RejectCompany
        [HttpPost("RejectCompany/{id}")]
        public async Task<ActionResult> RejectCompany([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
            //if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is already rejected"));
            //if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is approved"));
            user.IsApprove = IsApprove.rejected;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        #endregion

        #region Get Company By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound(new ApiResponse(404, "User not found"));

            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));

            var company = _mapper.Map<CompanyByIdOutputDto>(user);
            if (company == null) return NotFound(new ApiResponse(404, "Company not found"));

            // Roles
            var roles = await _userManager.GetRolesAsync(user);

            company.Roles = roles;


            return Ok(company);

        }
        #endregion


        #region Get All Companies
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            //get all companies where isDeleted is false
            var users = await _userManager.GetUsersInRoleAsync("Company");
            var notDeleted = users.Where(c => c.IsDeleted == false).ToList();
            var companies = _mapper.Map<List<CompanyByIdOutputDto>>(notDeleted);
            return Ok(companies);
        }
        #endregion

        #region Get All Companies by Status
        [HttpGet("status/{status}")]

        public async Task<IActionResult> GetAllCompaniesByStatus([FromRoute] string status)
        {
            var users = await _userManager.GetUsersInRoleAsync("Company");
            var companies = _mapper.Map<List<CompanyByIdOutputDto>>(users);

            if (!Enum.TryParse<IsApprove>(status.ToString(), true, out var isApprove))
            {
                return BadRequest(new ApiResponse(400, $"Invalid status, status must be one of {string.Join(", ", Enum.GetNames(typeof(IsApprove)))}"));
            }

            companies = companies.Where(c => c.IsApprove == isApprove).ToList();

            return Ok(companies);
        }
        #endregion

        #region Delete Company
        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCompany(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
			if (user is null) return NotFound(new ApiResponse(404, "User not found"));
			if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
			user.IsDeleted = true;
			await _userManager.UpdateAsync(user);
			return Ok();
		}
        #endregion

        #region get Company's Users
        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetCompanyUsers(int id)
        {
            IEnumerable<UserRequest> requestList = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            List<UserRequest> result = requestList
                .Where(r => r.InsurancePlan.CompanyId == $"{id}")
                .ToList();

            if (result.Count == 0)
            {
                return NotFound(new ApiResponse(404, "No requests yet"));
            }

            List<CompanyUsersDTO> users = result.Select(user => new CompanyUsersDTO
            {
                name = user.Customer?.Name,
                email = user.Customer?.Email,
                phone = user.Customer?.PhoneNumber
            }).ToList();

            return Ok(users);
        }
        #endregion

    }
}
