using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInsuranceController : ControllerBase
    {

        private readonly IGenericRepository<HealthInsurancePlan> HealthInsurancePlanRepository;
        public HealthInsuranceController(IGenericRepository<HealthInsurancePlan> _HealthInsurancePlanRepository)
        {
            HealthInsurancePlanRepository = _HealthInsurancePlanRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddHealthPlan(CreateHealthInsuranceDTO healthInsuranceDTO)
        {
            if (ModelState.IsValid)
            {
                var healthInsurancePlan = new HealthInsurancePlan
                {
                    YearlyCoverage = healthInsuranceDTO.YearlyCoverage,
                    Level = healthInsuranceDTO.Level,
                    CategoryId = healthInsuranceDTO.CategoryId,
                    Quotation = healthInsuranceDTO.Quotation,
                    CompanyId = healthInsuranceDTO.CompanyId,
                    MedicalNetwork=healthInsuranceDTO.MedicalNetwork,
                    ClinicsCoverage= healthInsuranceDTO.ClinicsCoverage,
                    HospitalizationAndSurgery=healthInsuranceDTO.HospitalizationAndSurgery,
                    OpticalCoverage=healthInsuranceDTO.OpticalCoverage,
                    DentalCoverage=healthInsuranceDTO.DentalCoverage

                };

                await HealthInsurancePlanRepository.AddAsync(healthInsurancePlan);
                return Ok(healthInsuranceDTO);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

