using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorInsuranceController : ControllerBase
    {

        private readonly IGenericRepository<MotorInsurancePlan> motorInsurancePlanRepository;
        public MotorInsuranceController(IGenericRepository<MotorInsurancePlan> _motorInsurancePlanRepository)
        {
            motorInsurancePlanRepository = _motorInsurancePlanRepository;
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

                await motorInsurancePlanRepository.AddAsync(motorInsurancePlan);
                return Ok(motorInsuranceDTO);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
