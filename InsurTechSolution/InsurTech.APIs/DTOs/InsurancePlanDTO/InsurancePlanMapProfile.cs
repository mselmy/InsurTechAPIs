using AutoMapper;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.InsurancePlanDTO
{
	public class InsurancePlanMapProfile : Profile
	{
		public InsurancePlanMapProfile()
		{
			CreateMap<InsurancePlan, InsurancePlanDTO>();
		
		}
	}
}
