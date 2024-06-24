using AutoMapper;

namespace InsurTech.APIs.DTOs.CompanyUpdateDto
{
	public class CompanyUpdateDtoMapProfile : Profile
	{
		public CompanyUpdateDtoMapProfile()
		{
			CreateMap<CompanyUpdateDto, Core.Entities.Identity.Company>();
		}
	}
}
