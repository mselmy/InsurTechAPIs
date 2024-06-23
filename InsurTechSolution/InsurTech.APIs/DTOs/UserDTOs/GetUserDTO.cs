using InsurTech.Core.Entities.Identity;

namespace InsurTech.APIs.DTOs.UserDTOs
{
	public class GetUserDTO
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Id { get; set; }
		public string UserType { get; set; }
	}
}
