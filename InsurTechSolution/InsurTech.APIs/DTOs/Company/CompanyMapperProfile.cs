using AutoMapper;
using InsurTech.Core.Entities.Identity;

namespace InsurTech.APIs.DTOs.Company

{
    public class CompanyMapperProfile : Profile
    {
        public CompanyMapperProfile()
        {
            CreateMap<Core.Entities.Identity.Company, RegisterCompanyInput>();

            CreateMap<Core.Entities.Identity.Company, CompanyByIdOutputDto>()
                .ForMember(dest => dest.InsurancePlansCount, opt => opt.MapFrom(src => src.InsurancePlans.Count));





        }


    }
}
