using AutoMapper;
using InsurTech.APIs.DTOs.FAQDTOs;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InsurTech.APIs.Controllers
{
    [Route("api/FAQs")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FAQController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
