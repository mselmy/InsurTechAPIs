using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.DTOs.Customer
{
	public class RegisterCustomerInput
	{
		[Required]
		[StringLength(AppUser.MaxNameLength)]
		public string Name { get; set; }
		[Required]
		[StringLength(AppUser.MaxNameLength)]
		[RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Invalid UserName")]
		public string UserName { get; set; }
		[Required]
		[EmailAddress]
		[StringLength(AppUser.MaxEmailAddressLength)]
		public string EmailAddress { get; set; }
		[Required]
		[StringLength(AppUser.MaxPlainPasswordLength)]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
		public string Password { get; set; }
		[Required]
		[StringLength(14)]
		public string NationalId { get; set; }

		public DateOnly BirthDate { get; set; }
		[JsonIgnore]
		public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
		[Required]
		[RegularExpression(@"^01(0|1|2|5)[0-9]{8}$")]
		public string PhoneNumber { get; set; }
	}
}
