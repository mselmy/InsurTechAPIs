using AutoMapper;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.ArticleDTOs
{
    public class ArticleMapProfile : Profile
    {
        public ArticleMapProfile()
        {
            CreateMap<CreateArticleInput, Article>();
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<CreateArticleWithImageInput, Article>();
        }

    }
}
