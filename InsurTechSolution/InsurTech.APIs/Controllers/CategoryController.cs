using AutoMapper;
using InsurTech.APIs.DTOs.Category;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
                var categoryDtos = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                
                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse(404));
            }
        }
    }
}
