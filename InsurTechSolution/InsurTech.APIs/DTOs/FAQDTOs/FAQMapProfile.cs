using AutoMapper;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.FAQDTOs
{
    public class FAQMapProfile : Profile
    {
        public FAQMapProfile() {

            CreateMap<CreateFAQInput, FAQ>();
            CreateMap<FAQ,FAQDTO>().ReverseMap();

        }
    }
}
