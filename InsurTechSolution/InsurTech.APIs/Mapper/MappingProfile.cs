using AutoMapper;
using InsurTech.APIs.DTOs.CompanyRequests;
using InsurTech.APIs.DTOs.Category;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.APIs.DTOs.NotificationDTO;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HealthInsurancePlan, HealthInsuranceDTO>()
                 .ForMember(dest => dest.NumberOfUsers, opt => opt.MapFrom(src => src.Requests.Count()))
                 .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company.UserName ?? "no company"))
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<HomeInsurancePlan, HomeInsuranceDTO>()
                .ForMember(dest => dest.NumberOfUsers, opt => opt.MapFrom(src => src.Requests.Count()))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company.UserName ?? "no company"))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<MotorInsurancePlan, MotorInsuranceDTO>()
                .ForMember(dest => dest.NumberOfUsers, opt => opt.MapFrom(src => src.Requests.Count()))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company.UserName ?? "no company"))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<UserRequest, UserRequestDto>()
                          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ))
                          .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name ?? string.Empty))
                          .ForMember(dest => dest.InsurancePlanName, opt => opt.MapFrom(src => src.InsurancePlan.Category.Name ?? string.Empty))
                          .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.InsurancePlan.Company.Name ?? string.Empty))
                          .ForMember(dest => dest.RequestQuestions, opt => opt.MapFrom(src => src.RequestQuestions))
                          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                          .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.InsurancePlan.Level));

            CreateMap<RequestQuestion, RequestQuestionDto>()
                       .ForMember(req => req.Question, opt => opt.MapFrom(src => src.Question.Body ?? string.Empty))
                       .ForMember(req => req.Answer, opt => opt.MapFrom(src => src.Answer ?? string.Empty));

                       

            CreateMap<Category, CategoryDTO>()
                 .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.InsurancePlans.Count))
                 .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.QuestionPlans.Count));

            CreateMap<Notification, GetNotificationDTO>();
 
        }
    }
}
