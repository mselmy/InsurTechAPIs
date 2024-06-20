using AutoMapper;

namespace InsurTech.APIs.DTOs.Customer
{
	public class GetCustomerMapperProfile : Profile
	{
		public GetCustomerMapperProfile()
		{
			CreateMap<Core.Entities.Identity.Customer, GetCustomerDTO>();
			CreateMap<GetCustomerDTO, Core.Entities.Identity.Customer>();
		}
	}
}
