using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
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
    public class HomeInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeInsuranceController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

                await unitOfWork.Repository<HomeInsurancePlan>().AddAsync(homeInsurancePlan);
                await unitOfWork.CompleteAsync();
                return Ok(homeInsuranceDTO);
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
    }
}

