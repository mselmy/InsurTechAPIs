using AutoMapper;

namespace InsurTech.APIs.DTOs.Question
{
	public class QuestionMapperProfile : Profile
	{
        public QuestionMapperProfile()
        {
            CreateMap<Core.Entities.Question, QuestionsDTO>();
            CreateMap<QuestionsDTO, Core.Entities.Question>();
        }
    }
}
