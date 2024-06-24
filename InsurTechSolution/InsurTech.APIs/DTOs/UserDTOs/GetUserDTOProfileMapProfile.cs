using AutoMapper;
using InsurTech.Core.Entities.Identity;

namespace InsurTech.APIs.DTOs.UserDTOs
{
	public class GetUserDTOProfileMapProfile : Profile
	{
		public GetUserDTOProfileMapProfile()
		{
			CreateMap<AppUser, GetUserDTO>().ReverseMap();
		}
	}
}
