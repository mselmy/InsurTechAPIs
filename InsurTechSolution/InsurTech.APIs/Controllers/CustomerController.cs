using AutoMapper;
using InsurTech.APIs.DTOs.Question;
using InsurTech.APIs.DTOs.RequestDTO;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InsurTech.APIs.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    //[Authorize(Roles=Roles.Customer)]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region GetQusetionsByCategory
        [HttpGet("GetQusetionsByCategory/{id}")]
        //[Authorize]
        //[Authorize(Roles = Roles.Customer)]
        //[Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> GetQusetionsByCategory([FromRoute] int id)
        {
            //var username = User.FindFirst(ClaimTypes.GivenName)?.Value;
            //Console.WriteLine($"username: \n{username}");
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine($"userId: \n{userId}");

            //var email = User.FindFirstValue(ClaimTypes.Email);
            //Console.WriteLine($"email: \n{email}");

            //var userType = User.FindFirstValue(ClaimTypes.UserData);
            //Console.WriteLine($"userType: \n{userType}");

            var questions = await _unitOfWork.Repository<Question>().GetAllAsync();
            var CategoryQuestions = questions.Where(q => q.CategoryId == id).ToList();
            var questionsDto = _mapper.Map<List<QuestionsDTO>>(CategoryQuestions);
            return Ok(questionsDto);
        }
        #endregion

        #region Request Insurance Plan
        [HttpPost("requestInsurancePlan")]
        public async Task<ActionResult> RequestAnswersWithInsurancePlan(ApplyForInsurancePlanInput applyForInsurancePlanInput)
        {
            try
            {
                var customerId = applyForInsurancePlanInput.CustomerId; //we could get current user id from token and remove this line + remove from ApplyForInsurancePlanInput + add [Authorize] attribute to the method

                //var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var insurancePlanId = applyForInsurancePlanInput.InsurancePlanId;

                var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();

                var existingRequest = userRequests.FirstOrDefault(r => r.CustomerId == customerId && r.InsurancePlanId == insurancePlanId);//we could add a method to the generic repository to get by predicate 

                if (existingRequest != null)
                {
                    return BadRequest(new ApiResponse(400, "Request Already Exists"));
                }

                await _unitOfWork.Repository<UserRequest>().AddAsync(new UserRequest { CustomerId = customerId, InsurancePlanId = insurancePlanId });
                

                await _unitOfWork.CompleteAsync();

                var allRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();

                //get the last request
                var userRequest = allRequests.LastOrDefault();

                if (userRequest == null)
                {
                   throw new Exception("Request Not Created");
                }

                var UserRequestAnswers = applyForInsurancePlanInput.Answers.Select(a => new RequestQuestion
                {
                    QuestionId = a.QuestionId,
                    Answer = a.Answer,
                    UserRequestId = userRequest.Id
                }).ToList();

                await _unitOfWork.Repository<RequestQuestion>().AddListAsync(UserRequestAnswers);

                await _unitOfWork.CompleteAsync();

                return Ok(new ApiResponse(200, "Request Created Successfully"));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Repository<UserRequest>().Delete(new UserRequest { CustomerId = applyForInsurancePlanInput.CustomerId, InsurancePlanId = applyForInsurancePlanInput.InsurancePlanId });

                await _unitOfWork.CompleteAsync();

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        #endregion
    }
}
