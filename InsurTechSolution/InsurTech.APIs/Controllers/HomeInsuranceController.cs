using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeInsuranceController : ControllerBase
    {

        private readonly IGenericRepository<HomeInsurancePlan> HomeInsurancePlanRepository;
        public HomeInsuranceController(IGenericRepository<HomeInsurancePlan> _homeInsurancePlanRepository)
        {
            HomeInsurancePlanRepository = _homeInsurancePlanRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddHomePlan(CreateHomeInsuranceDTO homeInsuranceDTO)
        {
            if (ModelState.IsValid)
            {
                var homeInsurancePlan = new HomeInsurancePlan
                {
                    YearlyCoverage = homeInsuranceDTO.YearlyCoverage,
                    Level = homeInsuranceDTO.Level,
                    CategoryId = homeInsuranceDTO.CategoryId,
                    Quotation = homeInsuranceDTO.Quotation,
                    CompanyId = homeInsuranceDTO.CompanyId,
                    WaterDamage= homeInsuranceDTO.WaterDamage,
                    GlassBreakage= homeInsuranceDTO.GlassBreakage,
                    NaturalHazard= homeInsuranceDTO.NaturalHazard,
                    AttemptedTheft= homeInsuranceDTO.AttemptedTheft,
                    FiresAndExplosion= homeInsuranceDTO.FiresAndExplosion

                };

                await HomeInsurancePlanRepository.AddAsync(homeInsurancePlan);
                return Ok(homeInsuranceDTO);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

