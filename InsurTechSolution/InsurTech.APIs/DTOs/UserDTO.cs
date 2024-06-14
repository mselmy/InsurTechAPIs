using InsurTech.Core.Entities.Identity;

namespace InsurTech.APIs.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Id { get; set; }
        public UserType UserType { get; set; }

    }
}
