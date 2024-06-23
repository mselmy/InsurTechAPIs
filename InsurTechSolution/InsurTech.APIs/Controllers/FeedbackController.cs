using AutoMapper;
using InsurTech.APIs.DTOs.FeedbackDTOs;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            var feedbacks = await _unitOfWork.Repository<Feedback>().GetAllAsync();
            var feedbackDtos = _mapper.Map<List<FeedbackDto>>(feedbacks);

            return Ok(feedbackDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(int id)
        {
            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);
            return Ok(feedbackDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var customerId = "1"; for testing


            // Check if the user is authorized to rate this insurance plan
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var userRequest = userRequests.FirstOrDefault(r => r.CustomerId == customerId
                                                             && r.InsurancePlanId == input.InsurancePlanId
                                                             && r.Status == RequestStatus.Approved);

            if (userRequest == null)
                return BadRequest("You cannot rate this insurance plan as you haven't purchased it.");

            var feedback = _mapper.Map<Feedback>(input);
            feedback.CustomerId = customerId;

            await _unitOfWork.Repository<Feedback>().AddAsync(feedback);
            await _unitOfWork.CompleteAsync();

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

            return Ok(new { Message = "Feedback submitted successfully", Feedback = feedbackDto });
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] CreateFeedbackInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (feedback.CustomerId != customerId && !User.IsInRole("Admin"))
            {
                return Forbid("You can only update your own feedback unless you are an admin.");
            }

            _mapper.Map(input, feedback);
            await _unitOfWork.CompleteAsync();

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

            return Ok(new { Message = "Feedback updated successfully", Feedback = feedbackDto });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (feedback.CustomerId != customerId && !User.IsInRole("Admin"))
            {
                return Forbid("You can only delete your own feedback unless you are an admin.");
            }

            _unitOfWork.Repository<Feedback>().Delete(feedback);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Feedback deleted successfully" });
        }
    }
}
