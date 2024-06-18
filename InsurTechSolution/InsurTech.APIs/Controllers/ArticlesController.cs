using AutoMapper;
using InsurTech.APIs.DTOs.ArticleDTOs;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Swashbuckle.AspNetCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsurTech.APIs.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticlesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _unitOfWork.Repository<Article>().GetAllAsync();

            var articlesDto = _mapper.Map<List<ArticleDto>>(articles);

            return Ok(articlesDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _unitOfWork.Repository<Article>().GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            var articleDto = _mapper.Map<ArticleDto>(article);

            return Ok(articleDto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleInput articleInput)
        {
            var article = _mapper.Map<Article>(articleInput);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            article.UserId = userId;

            article.Date = DateOnly.FromDateTime(DateTime.Now);

            await _unitOfWork.Repository<Article>().AddAsync(article);

            await _unitOfWork.CompleteAsync();

            var articleDto = _mapper.Map<ArticleDto>(article);

            return Ok(articleDto);
        }
    
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> UpdateArticle(int id, [FromBody] CreateArticleInput articleInput)
        {
            var article = await _unitOfWork.Repository<Article>().GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            _mapper.Map(articleInput, article);

            await _unitOfWork.CompleteAsync();

            var articleDto = _mapper.Map<ArticleDto>(article);

            return Ok(articleDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _unitOfWork.Repository<Article>().GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Article>().Delete(article);

            await _unitOfWork.CompleteAsync();

            return Ok(new ApiResponse(200, "Article deleted successfully"));
        }

    }
}
