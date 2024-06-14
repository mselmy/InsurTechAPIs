using InsurTech.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.DTOs.Company
{
	public class RegisterCompanyInput
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
		[RegularExpression(@"^(\d{9})$", ErrorMessage = "Invalid Tax Number")]

		public string TaxNumber { get; set; }
		[Required]
		public string Location { get; set; }

		[JsonIgnore]
		public IsApprove Status { get; set; } = IsApprove.pending;
		[Required]
		[RegularExpression(@"^01(0|1|2|5)[0-9]{8}$")]
		public string phoneNumber { get; set; }
	}
}
