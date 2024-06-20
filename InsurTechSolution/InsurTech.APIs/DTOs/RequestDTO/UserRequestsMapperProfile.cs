using AutoMapper;

namespace InsurTech.APIs.DTOs.RequestDTO
{
	public class UserRequestsMapperProfile : Profile
	{
		public UserRequestsMapperProfile()
		{
			CreateMap<Core.Entities.UserRequest, UserRequestDTO>();
			CreateMap<UserRequestDTO, Core.Entities.UserRequest>();
		}
	}
}
