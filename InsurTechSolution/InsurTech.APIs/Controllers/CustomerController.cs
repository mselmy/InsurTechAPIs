using AutoMapper;
using InsurTech.APIs.DTOs.Question;
using InsurTech.APIs.DTOs.RequestDTO;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<ActionResult> GetQusetionsByCategory([FromRoute] int id)
		{
			var questions = await _unitOfWork.Repository<Question>().GetAllAsync();
			var CategoryQuestions = questions.Where(q => q.CategoryId == id).ToList();
			var questionsDto = _mapper.Map<List<QuestionsDTO>>(CategoryQuestions);
			return Ok(questionsDto);
		}
		#endregion

		#region RequestAnswersWithInsurancePlan
		[HttpPost("RequestAnswersWithInsurancePlan")]
		public async Task<ActionResult> RequestAnswersWithInsurancePlan(List<RequestAnswersDto> answers)
		{
			var Userrequest = _unitOfWork.Repository<UserRequest>().AddAsync(new UserRequest());
			var UserRequestAnswers = answers.Select(a => new RequestQuestion
			{
				QuestionId = a.QuestionId,
				Answer = a.Answer,
				UserRequestId = Userrequest.Id
			}).ToList();
			await _unitOfWork.Repository<RequestQuestion>().AddListAsync(UserRequestAnswers);
			


			return Ok(UserRequestAnswers);
		}
		#endregion
	}
}
