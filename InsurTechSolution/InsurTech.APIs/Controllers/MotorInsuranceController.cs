using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;


        public MotorInsuranceController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("AddMotorPlan")]
        public async Task<IActionResult> AddMotorPlan(CreateMotorInsuranceDTO motorInsuranceDTO)
        {
            if (ModelState.IsValid)
            {
                var motorInsurancePlan = new MotorInsurancePlan
                {
                    YearlyCoverage = motorInsuranceDTO.YearlyCoverage,
                    Level = motorInsuranceDTO.Level,
                    CategoryId = motorInsuranceDTO.CategoryId,
                    Quotation = motorInsuranceDTO.Quotation,
                    CompanyId = motorInsuranceDTO.CompanyId,
                    PersonalAccident = motorInsuranceDTO.PersonalAccident,
                    Theft = motorInsuranceDTO.Theft,
                    ThirdPartyLiability = motorInsuranceDTO.ThirdPartyLiability,
                    OwnDamage = motorInsuranceDTO.OwnDamage,
                    LegalExpenses = motorInsuranceDTO.LegalExpenses,
                    AvailableInsurance=true
                };

                await _unitOfWork.Repository<MotorInsurancePlan>().AddAsync(motorInsurancePlan);
                await _unitOfWork.CompleteAsync();
                return Ok(motorInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditMotorPlan(int id, EditMotorInsuranceDTO motorInsuranceDTO)
        {
            if (id <= 0 || motorInsuranceDTO == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var storedMotorInsurancePlan = await _unitOfWork.Repository<MotorInsurancePlan>().GetByIdAsync(id);
                if (storedMotorInsurancePlan == null)
                {
                    return NotFound();
                }

                storedMotorInsurancePlan.YearlyCoverage = motorInsuranceDTO.YearlyCoverage;
                storedMotorInsurancePlan.Level = motorInsuranceDTO.Level;
                storedMotorInsurancePlan.CategoryId = motorInsuranceDTO.CategoryId;
                storedMotorInsurancePlan.Quotation = motorInsuranceDTO.Quotation;
                storedMotorInsurancePlan.CompanyId = motorInsuranceDTO.CompanyId;
                storedMotorInsurancePlan.PersonalAccident = motorInsuranceDTO.PersonalAccident;
                storedMotorInsurancePlan.Theft = motorInsuranceDTO.Theft;
                storedMotorInsurancePlan.ThirdPartyLiability = motorInsuranceDTO.ThirdPartyLiability;
                storedMotorInsurancePlan.OwnDamage = motorInsuranceDTO.OwnDamage;
                storedMotorInsurancePlan.LegalExpenses = motorInsuranceDTO.LegalExpenses;

                await _unitOfWork.Repository<MotorInsurancePlan>().Update(storedMotorInsurancePlan);
                await _unitOfWork.CompleteAsync();

                return Ok(motorInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorInsuranceById(int id)
        {

            var motorInsurance = await _unitOfWork.Repository<MotorInsurancePlan>().GetByIdAsync(id);
            if(motorInsurance!=null || motorInsurance?.AvailableInsurance != false)
            {
                var NumberOfRequests = motorInsurance.Requests.Count();
                MotorInsuranceDTO motorinsuranceDto = new MotorInsuranceDTO()
                {
                    NumberOfUsers = NumberOfRequests,
                    Id = motorInsurance.Id,
                    Level = motorInsurance.Level,
                    LegalExpenses = motorInsurance.LegalExpenses,
                    OwnDamage = motorInsurance.OwnDamage,
                    PersonalAccident = motorInsurance.PersonalAccident,
                    ThirdPartyLiability = motorInsurance.ThirdPartyLiability,
                    Theft = motorInsurance.Theft,
                    Quotation = motorInsurance.Quotation,
                    YearlyCoverage = motorInsurance.YearlyCoverage,
                    Category = motorInsurance.Category.Name,
                    Company = motorInsurance.Company?.UserName ?? "no comapny"
                };
                return Ok(motorinsuranceDto);
            }
            else
            {
                return BadRequest("No Matches Insurances found");
            }
        }

        [HttpGet("GetMotorInsurance")]
        public async Task<IActionResult> GetMotorInsurance()
        {
            var motorInsurance = await _unitOfWork.Repository<MotorInsurancePlan>().GetAllAsync();
            motorInsurance = motorInsurance.Where(a => a.AvailableInsurance == true).ToList();
            if (motorInsurance.Count() != 0)
            {
                List<MotorInsuranceDTO> motorinsuranceDto = new List<MotorInsuranceDTO>();

                foreach (var item in motorInsurance)
                {
                    var NumberOfRequests = item.Requests.Count();
                    var motorinsuranceitem =new MotorInsuranceDTO()
                    {
                        Id = item.Id,
                        Level = item.Level,
                        LegalExpenses = item.LegalExpenses,
                        OwnDamage = item.OwnDamage,
                        PersonalAccident = item.PersonalAccident,
                        ThirdPartyLiability = item.ThirdPartyLiability,
                        Theft = item.Theft,
                        Quotation = item.Quotation,
                        YearlyCoverage = item.YearlyCoverage,
                        Category = item.Category.Name,
                        Company = item.Company?.UserName ?? "no comapny",
                        NumberOfUsers = NumberOfRequests,


                    };
                    motorinsuranceDto.Add(motorinsuranceitem);
                }
                return Ok(motorinsuranceDto);
            }
            else
            {
                return BadRequest("No Insurances Yet");
            }
        }


    }
}
