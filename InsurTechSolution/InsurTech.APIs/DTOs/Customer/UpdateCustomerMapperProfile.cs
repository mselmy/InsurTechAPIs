using AutoMapper;

namespace InsurTech.APIs.DTOs.Customer
{
	public class UpdateCustomerMapperProfile : Profile
	{
		public UpdateCustomerMapperProfile()
		{
			CreateMap<UpdateCustomerDTO, Core.Entities.Identity.Customer>();
			CreateMap<Core.Entities.Identity.Customer, UpdateCustomerDTO>();
		}

	}
}
