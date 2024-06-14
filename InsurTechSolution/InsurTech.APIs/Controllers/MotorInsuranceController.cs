using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MotorInsuranceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
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
                    LegalExpenses = motorInsuranceDTO.LegalExpenses
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
    }
}
