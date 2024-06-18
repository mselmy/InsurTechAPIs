using AutoMapper;

namespace InsurTech.APIs.DTOs.RequestQuestions
{
	public class RequestQuestionMapperProfile : Profile
	{
		public RequestQuestionMapperProfile()
		{
			CreateMap<Core.Entities.Question, RequestQuestionDTO>();
			CreateMap<RequestQuestionDTO, Core.Entities.Question>();
		}
	}
}
