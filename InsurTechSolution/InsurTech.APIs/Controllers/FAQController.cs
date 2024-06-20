using AutoMapper;
using InsurTech.APIs.DTOs.FAQDTOs;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsurTech.APIs.Controllers
{
    [Route("api/FAQs")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public FAQController(IUnitOfWork unitOfWork, IMapper mapper,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetFAQs()
        {
            var faqs = await _unitOfWork.Repository<FAQ>().GetAllAsync();

            var faqDtos = _mapper.Map<List<FAQDTO>>(faqs);

            return Ok(faqDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFAQ(int id)
        {
            var faq = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            var faqDto = _mapper.Map<FAQDTO>(faq);

            return Ok(faqDto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateFAQ([FromBody] CreateFAQInput createFAQInput)
        {
            var faq = _mapper.Map<FAQ>(createFAQInput);

            faq.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _unitOfWork.Repository<FAQ>().AddAsync(faq);

            await _unitOfWork.CompleteAsync();

            var faqDto = _mapper.Map<FAQDTO>(faq);

            // Create and send notifications

            // Fetch all approved customers
            var approvedCustomers = await _userManager.Users
                                       .Where(u => u.UserType == UserType.Customer && u.IsApprove == IsApprove.approved)
                                       .ToListAsync();

            foreach (var customer in approvedCustomers)
            {
                var notification = new Notification
                {
                    Body = $"A new FAQ titled '{faq.Body}' has been added. Check it out!",
                    UserId = customer.Id,
                    IsRead = false
                };

                await _unitOfWork.Repository<Notification>().AddAsync(notification);
            }

            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetFAQ), new { id = faq.Id }, faqDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> UpdateFAQ([FromRoute] int id, [FromBody] CreateFAQInput createFAQInput)
        {
            var faq = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);

            if (faq == null)
            { return NotFound(); }

            _mapper.Map<CreateFAQInput, FAQ>(createFAQInput, faq);

            await _unitOfWork.CompleteAsync();

            var faqDto = _mapper.Map<FAQDTO>(faq);

            return Ok(faqDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> DeleteFAQ(int id)
        {
            var faq = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);

            if (faq == null)
            { return NotFound(); }


            await _unitOfWork.Repository<FAQ>().Delete(faq);

            await _unitOfWork.CompleteAsync();


            return Ok("FAQ deleted successfully.");

        }

    }
}
