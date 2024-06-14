using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public HealthInsuranceController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

                await unitOfWork.Repository<HealthInsurancePlan>().AddAsync(healthInsurancePlan);
                await unitOfWork.CompleteAsync();
                return Ok(healthInsuranceDTO);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditHealthPlan(int id, EditHealthInsuranceDTO HealthInsuranceDTO)
        {
            if (id <= 0 || HealthInsuranceDTO == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var storedHealthInsurancePlan = await unitOfWork.Repository<HealthInsurancePlan>().GetByIdAsync(id);
                if (storedHealthInsurancePlan == null)
                {
                    return NotFound();
                }

                storedHealthInsurancePlan.YearlyCoverage = HealthInsuranceDTO.YearlyCoverage;
                storedHealthInsurancePlan.Level = HealthInsuranceDTO.Level;
                storedHealthInsurancePlan.CategoryId = HealthInsuranceDTO.CategoryId;
                storedHealthInsurancePlan.Quotation = HealthInsuranceDTO.Quotation;
                storedHealthInsurancePlan.CompanyId = HealthInsuranceDTO.CompanyId;
                storedHealthInsurancePlan.MedicalNetwork = HealthInsuranceDTO.MedicalNetwork;
                storedHealthInsurancePlan.ClinicsCoverage = HealthInsuranceDTO.ClinicsCoverage;
                storedHealthInsurancePlan.HospitalizationAndSurgery = HealthInsuranceDTO.HospitalizationAndSurgery;
                storedHealthInsurancePlan.OpticalCoverage = HealthInsuranceDTO.OpticalCoverage;
                storedHealthInsurancePlan.DentalCoverage = HealthInsuranceDTO.DentalCoverage;

                await unitOfWork.Repository<HealthInsurancePlan>().Update(storedHealthInsurancePlan);
                await unitOfWork.CompleteAsync();

                return Ok(HealthInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



    }
}

