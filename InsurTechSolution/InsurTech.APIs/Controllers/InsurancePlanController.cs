using AutoMapper;
using Google.Apis.Auth.OAuth2;
using InsurTech.APIs.DTOs;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.InsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using InsurTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public InsurancePlanController(IUnitOfWork unitOfWork , IMapper mapper , UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region GetInsurancePlansByCategoryId
        [HttpGet("GetInsurancePlansByCategoryId/{id}")]
		public async Task<IActionResult> GetInsurancePlansByCategoryId(int id)
        {
			try
            {
                var insurancePlans = await _unitOfWork.Repository<InsurancePlan>().GetAllAsync();
                dynamic filteredInsurancePlans = insurancePlans.Where(plan => plan.CategoryId == id && plan.AvailableInsurance == true).ToList();
                switch(id)
                {
					case 1:
                        List<HealthInsuranceDTO> healthInsuranceDtos = [];
                        foreach(var plan in filteredInsurancePlans)
                        {
							var healthInsuranceDto = new HealthInsuranceDTO
                            {
								Id = plan.Id,
								YearlyCoverage = plan.YearlyCoverage,
								Level = plan.Level,
								Category = plan.Category.Name,
								Quotation = plan.Quotation,
								Company = plan.Company.Name,
								MedicalNetwork = plan.MedicalNetwork,
								ClinicsCoverage = plan.ClinicsCoverage,
								HospitalizationAndSurgery = plan.HospitalizationAndSurgery,
								OpticalCoverage = plan.OpticalCoverage,
								DentalCoverage = plan.DentalCoverage,
							};
							healthInsuranceDtos.Add(healthInsuranceDto);
						}
						return Ok(healthInsuranceDtos);
					case 2:
                        List<HomeInsuranceDTO> homeInsuranceDtos = [];
                        foreach(var plan in filteredInsurancePlans)
                        {
                            var homeInsuranceDto = new HomeInsuranceDTO
                            {	Id = plan.Id,
								YearlyCoverage = plan.YearlyCoverage,
								Level = plan.Level,
								Category = plan.Category.Name,
								Quotation = plan.Quotation,
								Company = plan.Company.Name,
								WaterDamage = plan.WaterDamage,
								GlassBreakage = plan.GlassBreakage,
								NaturalHazard = plan.NaturalHazard,
								AttemptedTheft = plan.AttemptedTheft,
								FiresAndExplosion = plan.FiresAndExplosion,
							};
                            homeInsuranceDtos.Add(homeInsuranceDto);
                        }
						return Ok(homeInsuranceDtos);
					case 3:
                        List<MotorInsuranceDTO> motorInsuranceDtos = [];
						foreach(var plan in filteredInsurancePlans)
                        {
							var motorInsuranceDto = new MotorInsuranceDTO
                            {
								Id = plan.Id,
								YearlyCoverage = plan.YearlyCoverage,
								Level = plan.Level,
								Category = plan.Category.Name,
								Quotation = plan.Quotation,
								Company = plan.Company.Name,
								PersonalAccident = plan.PersonalAccident,
								Theft = plan.Theft,
								ThirdPartyLiability = plan.ThirdPartyLiability,
								OwnDamage = plan.OwnDamage,
								LegalExpenses = plan.LegalExpenses,
							};
							motorInsuranceDtos.Add(motorInsuranceDto);
						}
						return Ok(motorInsuranceDtos);
					default:
						return NotFound(new ApiResponse(404, "No insurance plans found for the specified category."));
				}
			}
			catch (Exception)
            {
				return StatusCode(500, new ApiResponse(500, "An error occurred while retrieving insurance plans."));
			}
		}
		#endregion


		[HttpDelete("DeleteInsurancePlan/{id}")]
        public async Task<IActionResult> DeleteInsurancePlan(int id)
        {
            try
            {
                var plan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(id);
                if (plan is null) return NotFound(new ApiResponse(400, "Insurance Plan Not Found"));


                plan.AvailableInsurance = false;
                await _unitOfWork.Repository<InsurancePlan>().Update(plan);
                await _unitOfWork.CompleteAsync();

                var notification = new Notification
                {
                    Body = $"The insurance plan  with ID { plan.Id } has been deleted by company ID { plan.CompanyId }.",
                    UserId = "1" ,
                    IsRead = false
                };
                await _unitOfWork.Repository<Notification>().AddAsync(notification);
                await _unitOfWork.CompleteAsync();

                return Ok(new ApiResponse(200, "Deleted"));
            }
           
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, "An error occurred while deleting the insurance plan."));
            }
        }

        [HttpGet("InsurancePlansByCompanyId/{id}")]
        public async Task<IActionResult> InsurancePlansByCompanyId(string id)
        {
            try
            {
                var healthInsurancePlans = await _unitOfWork.Repository<HealthInsurancePlan>().GetAllAsync();
                var filteredHealthPlans = healthInsurancePlans
                    .Where(plan => plan.AvailableInsurance && plan.CompanyId == id).ToList();
                var healthInsuranceDtos = _mapper.Map<List<HealthInsuranceDTO>>(filteredHealthPlans);

                var homeInsurancePlans = await _unitOfWork.Repository<HomeInsurancePlan>().GetAllAsync();
                var filteredHomePlans = homeInsurancePlans
                    .Where(plan => plan.AvailableInsurance && plan.CompanyId == id).ToList();
                var homeInsuranceDtos = _mapper.Map<List<HomeInsuranceDTO>>(filteredHomePlans);

                var motorInsurancePlans = await _unitOfWork.Repository<MotorInsurancePlan>().GetAllAsync();
                var filteredMotorPlans = motorInsurancePlans
                    .Where(plan => plan.AvailableInsurance && plan.CompanyId == id).ToList();
                var motorInsuranceDtos = _mapper.Map<List<MotorInsuranceDTO>>(filteredMotorPlans);

                if (healthInsuranceDtos.Count == 0 && homeInsuranceDtos.Count == 0 && motorInsuranceDtos.Count == 0)
                {
                    return NotFound(new ApiResponse(404, "No Insurances Yet"));
                }

                return Ok(new InsurancePlanByUserIdDTO
                {
                    HealthInsurancePlans = healthInsuranceDtos,
                    HomeInsurancePlans = homeInsuranceDtos,
                    MotorInsurancePlans = motorInsuranceDtos
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "An error occurred while retrieving insurance plans."));
            }
        }


        [HttpGet("InsurancePlansByCustomerId/{id}")]
        public async Task<IActionResult> InsurancePlansByCustomerId(string id)
        {
            try
            {
                var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
                var soldInsurancePlanIds = userRequests.Where(ur => ur.CustomerId == id).Select(ur => ur.InsurancePlanId).ToList();

                var healthInsurancePlans = await _unitOfWork.Repository<HealthInsurancePlan>().GetAllAsync();
                var filteredHealthPlans = healthInsurancePlans
                    .Where(plan => soldInsurancePlanIds.Contains(plan.Id)).ToList();
                var healthInsuranceDtos = _mapper.Map<List<HealthInsuranceDTO>>(filteredHealthPlans);

                var homeInsurancePlans = await _unitOfWork.Repository<HomeInsurancePlan>().GetAllAsync();
                var filteredHomePlans = homeInsurancePlans
                    .Where(plan => soldInsurancePlanIds.Contains(plan.Id)).ToList();
                var homeInsuranceDtos = _mapper.Map<List<HomeInsuranceDTO>>(filteredHomePlans);

                var motorInsurancePlans = await _unitOfWork.Repository<MotorInsurancePlan>().GetAllAsync();
                var filteredMotorPlans = motorInsurancePlans
                    .Where(plan => soldInsurancePlanIds.Contains(plan.Id)).ToList();
                var motorInsuranceDtos = _mapper.Map<List<MotorInsuranceDTO>>(filteredMotorPlans);

                if (healthInsuranceDtos.Count == 0 && homeInsuranceDtos.Count == 0 && motorInsuranceDtos.Count == 0)
                {
                    return NotFound(new ApiResponse(404, "No Insurances Yet"));
                }

                return Ok(new InsurancePlanByUserIdDTO
                {
                    HealthInsurancePlans = healthInsuranceDtos,
                    HomeInsurancePlans = homeInsuranceDtos,
                    MotorInsurancePlans = motorInsuranceDtos
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "An error occurred while retrieving insurance plans."));
            }
        }
        [HttpGet("SoldInsuranceByCompanyId/{companyId}")]
        public async Task<IActionResult> SoldInsuranceByCompanyId(string companyId)
        {
            try
            {
                var companyInsurancePlans = await _unitOfWork.Repository<InsurancePlan>().GetAllAsync();
                var insurancePlansForCompany = companyInsurancePlans.Where(plan => plan.CompanyId == companyId).ToList();

                if (!insurancePlansForCompany.Any())
                {
                    return NotFound(new ApiResponse(404, "No insurance plans found for the specified company."));
                }

                var insurancePlanIds = insurancePlansForCompany.Select(plan => plan.Id).ToList();

                var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
                var soldInsuranceRequests = userRequests.Where(request => insurancePlanIds.Contains(request.InsurancePlanId)).ToList();

                if (!soldInsuranceRequests.Any())
                {
                    return NotFound(new ApiResponse(404, "No sold insurance found for the specified company."));
                }

                var userIds = soldInsuranceRequests.Select(request => request.CustomerId).Distinct().ToList();

                // Dictionary to hold usernames for each insurance plan
                var insurancePlanUsernames = new Dictionary<int, List<string>>();

                foreach (var insurancePlan in insurancePlansForCompany)
                {
                    var usernames = new List<string>();
                    var userRequestsForPlan = soldInsuranceRequests.Where(request => request.InsurancePlanId == insurancePlan.Id).ToList();

                    foreach (var request in userRequestsForPlan)
                    {
                        var user = await _userManager.FindByIdAsync(request.CustomerId);
                        if (user != null)
                        {
                            usernames.Add(user.UserName);
                        }
                    }

                    insurancePlanUsernames[insurancePlan.Id] = usernames;
                }

                var result = insurancePlansForCompany.Select(plan => new InsurancePlanAndUserNameDTO
                {
                    InsurancePlan = new InsurancePlanForCompanyDTO
                    {
                        CategoryName=plan.Category.Name,
                        Level=plan.Level,
                        Quotation=plan.Quotation,

                    },
                    Usernames = insurancePlanUsernames[plan.Id]
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


    }
}
