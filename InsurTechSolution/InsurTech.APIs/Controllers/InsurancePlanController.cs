using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsurancePlanController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete("DeleteInsurancePlan")]
        public async Task<IActionResult> DeleteInsurancePlan(int id)
        {
            try
            {
                var plan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(id);
                if (plan is null) return NotFound(new ApiResponse(400, "Insurance Plan Not Found"));


                plan.AvailableInsurance = false;
                await _unitOfWork.Repository<InsurancePlan>().Update(plan);
                await _unitOfWork.CompleteAsync();

                return Ok(new ApiResponse(200, "Deleted"));
            }
           
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, "An error occurred while deleting the insurance plan."));
            }
        }


    }
}
