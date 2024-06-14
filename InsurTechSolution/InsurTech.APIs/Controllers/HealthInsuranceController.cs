using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
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
        [HttpPost("AddHealthPlan")]
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
                    MedicalNetwork = healthInsuranceDTO.MedicalNetwork,
                    ClinicsCoverage = healthInsuranceDTO.ClinicsCoverage,
                    HospitalizationAndSurgery = healthInsuranceDTO.HospitalizationAndSurgery,
                    OpticalCoverage = healthInsuranceDTO.OpticalCoverage,
                    DentalCoverage = healthInsuranceDTO.DentalCoverage,
                    AvailableInsurance = true

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



        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthInsuranceById(int id)
        {

            var HealthInsurance = await unitOfWork.Repository<HealthInsurancePlan>().GetByIdAsync(id);
            if (HealthInsurance != null || HealthInsurance?.AvailableInsurance != false)
            {
                var NumberOfRequests = HealthInsurance.Requests.Count();
                HealthInsuranceDTO HealthInsuranceDto = new HealthInsuranceDTO()
                {
                    NumberOfUsers = NumberOfRequests,
                    Id = HealthInsurance.Id,
                    Level = HealthInsurance.Level,
                    HospitalizationAndSurgery = HealthInsurance.HospitalizationAndSurgery,
                    ClinicsCoverage = HealthInsurance.ClinicsCoverage,
                    DentalCoverage = HealthInsurance.DentalCoverage,
                    OpticalCoverage = HealthInsurance.OpticalCoverage,
                    MedicalNetwork = HealthInsurance.MedicalNetwork,
                    Quotation = HealthInsurance.Quotation,
                    YearlyCoverage = HealthInsurance.YearlyCoverage,
                    Category = HealthInsurance.Category.Name,
                    Company = HealthInsurance.Company?.UserName ?? "no comapny"
                };
                return Ok(HealthInsuranceDto);
            }
            else
            {
                return BadRequest("No Matches Insurances found");
            }
        }


        [HttpGet("GetHealthInsurance")]
        public async Task<IActionResult> GetHealthInsurance()
        {
            var HealthInsurance = await unitOfWork.Repository<HealthInsurancePlan>().GetAllAsync();
            HealthInsurance=HealthInsurance.Where(a=>a.AvailableInsurance==true).ToList();
            if (HealthInsurance.Count()!=0)
            {
                List<HealthInsuranceDTO> HealthinsuranceDto = new List<HealthInsuranceDTO>();
                foreach (var item in HealthInsurance)
                {
                    var NumberOfRequests = item.Requests.Count();
                    var Healthinsuranceitem = new HealthInsuranceDTO()
                    {
                        Id = item.Id,
                        Level = item.Level,
                        HospitalizationAndSurgery = item.HospitalizationAndSurgery,
                        ClinicsCoverage = item.ClinicsCoverage,
                        DentalCoverage = item.DentalCoverage,
                        OpticalCoverage = item.OpticalCoverage,
                        MedicalNetwork = item.MedicalNetwork,
                        Quotation = item.Quotation,
                        YearlyCoverage = item.YearlyCoverage,
                        Category = item.Category.Name,
                        Company = item.Company?.UserName ?? "no comapny",
                        NumberOfUsers=NumberOfRequests
                    };
                    HealthinsuranceDto.Add(Healthinsuranceitem);
                }
                return Ok(HealthinsuranceDto);
            }
            else
            {
                return BadRequest("No Insurances Yet");
            }
        }

       
    }
}

