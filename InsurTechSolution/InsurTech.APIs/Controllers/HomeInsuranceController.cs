using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeInsuranceController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost("AddHomePlan")]
        public async Task<IActionResult> AddHomePlan(CreateHomeInsuranceDTO HomeInsuranceDTO)
        {
            if (ModelState.IsValid)
            {
                var HomeInsurancePlan = new HomeInsurancePlan
                {
                    YearlyCoverage = HomeInsuranceDTO.YearlyCoverage,
                    Level = HomeInsuranceDTO.Level,
                    CategoryId = HomeInsuranceDTO.CategoryId,
                    Quotation = HomeInsuranceDTO.Quotation,
                    CompanyId = HomeInsuranceDTO.CompanyId,
                    WaterDamage= HomeInsuranceDTO.WaterDamage,
                    GlassBreakage= HomeInsuranceDTO.GlassBreakage,
                    NaturalHazard= HomeInsuranceDTO.NaturalHazard,
                    AttemptedTheft= HomeInsuranceDTO.AttemptedTheft,
                    FiresAndExplosion= HomeInsuranceDTO.FiresAndExplosion,
                    AvailableInsurance=true

                };

                await unitOfWork.Repository<HomeInsurancePlan>().AddAsync(HomeInsurancePlan);
                await unitOfWork.CompleteAsync();
                return Ok(HomeInsuranceDTO);
            }
            else
            {
                return BadRequest();
            }
        }




        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditHomePlan(int id, EditHomeInsuranceDTO HomeInsuranceDTO)
        {
            if (id <= 0 || HomeInsuranceDTO == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var storedHomeInsurancePlan = await unitOfWork.Repository<HomeInsurancePlan>().GetByIdAsync(id);
                if (storedHomeInsurancePlan == null)
                {
                    return NotFound();
                }

                storedHomeInsurancePlan.YearlyCoverage = HomeInsuranceDTO.YearlyCoverage;
                storedHomeInsurancePlan.Level = HomeInsuranceDTO.Level;
                storedHomeInsurancePlan.CategoryId = HomeInsuranceDTO.CategoryId;
                storedHomeInsurancePlan.Quotation = HomeInsuranceDTO.Quotation;
                storedHomeInsurancePlan.CompanyId = HomeInsuranceDTO.CompanyId;
                storedHomeInsurancePlan.WaterDamage = HomeInsuranceDTO.WaterDamage;
                storedHomeInsurancePlan.GlassBreakage = HomeInsuranceDTO.GlassBreakage;
                storedHomeInsurancePlan.NaturalHazard = HomeInsuranceDTO.NaturalHazard;
                storedHomeInsurancePlan.AttemptedTheft = HomeInsuranceDTO.AttemptedTheft;
                storedHomeInsurancePlan.FiresAndExplosion = HomeInsuranceDTO.FiresAndExplosion;

                await unitOfWork.Repository<HomeInsurancePlan>().Update(storedHomeInsurancePlan);
                await unitOfWork.CompleteAsync();

                return Ok(HomeInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeInsuranceById(int id)
        {

            var HomeInsurance = await unitOfWork.Repository<HomeInsurancePlan>().GetByIdAsync(id);
            if (HomeInsurance != null)
            {
                var NumberOfRequests = HomeInsurance.Requests.Count();
                HomeInsuranceDTO HomeInsuranceDto = new HomeInsuranceDTO()
                {
                    NumberOfUsers = NumberOfRequests,
                    Id = HomeInsurance.Id,
                    Level = HomeInsurance.Level,
                    GlassBreakage = HomeInsurance.GlassBreakage,
                    AttemptedTheft = HomeInsurance.AttemptedTheft,
                    FiresAndExplosion = HomeInsurance.FiresAndExplosion,
                    NaturalHazard = HomeInsurance.NaturalHazard,
                    WaterDamage = HomeInsurance.WaterDamage,
                    Quotation = HomeInsurance.Quotation,
                    YearlyCoverage = HomeInsurance.YearlyCoverage,
                    Category = HomeInsurance.Category.Name,
                    Company = HomeInsurance.Company?.UserName ?? "no comapny"
                };
                return Ok(HomeInsuranceDto);
            }
            else
            {
                return BadRequest("No Matches Insurances found");
            }
        }



        [HttpGet("GetHomeInsurance")]
        public async Task<IActionResult> GetHomeInsurance()
        {
            var HomeInsurance = await unitOfWork.Repository<HomeInsurancePlan>().GetAllAsync();
            if (HomeInsurance.Count() != 0)
            {
                List<HomeInsuranceDTO> HomeinsuranceDto = new List<HomeInsuranceDTO>();
                foreach (var item in HomeInsurance)
                {
                    var NumberOfRequests = item.Requests.Count();
                    var Homeinsuranceitem = new HomeInsuranceDTO()
                    {
                        Id = item.Id,
                        Level = item.Level,
                        GlassBreakage = item.GlassBreakage,
                        AttemptedTheft = item.AttemptedTheft,
                        FiresAndExplosion = item.FiresAndExplosion,
                        NaturalHazard = item.NaturalHazard,
                        WaterDamage = item.WaterDamage,
                        Quotation = item.Quotation,
                        YearlyCoverage = item.YearlyCoverage,
                        Category = item.Category.Name,
                        Company = item.Company?.UserName ?? "no comapny",
                        NumberOfUsers=NumberOfRequests
                    };
                    HomeinsuranceDto.Add(Homeinsuranceitem);
                }
                return Ok(HomeinsuranceDto);
            }
            else
            {
                return BadRequest("No Insurances Yet");
            }
        }

    }
}

