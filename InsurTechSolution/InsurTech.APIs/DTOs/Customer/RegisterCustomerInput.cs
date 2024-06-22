using InsurTech.APIs.CustomeValidation;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using System.ComponentModel;
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
		[RegularExpression(@"^\d{14}$")]
		public string NationalId { get; set; }

		[Required]
		//month should be less than 13 and day should be less than 32
		//[RegularExpression( @"^(\d{4})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Invalid Date")]
		//[CheckBirthDate]
		public string BirthDate { get; set; }

		[Required]
		[RegularExpression(@"^01(0|1|2|5)[0-9]{8}$")]
		public string PhoneNumber { get; set; }
	}
}
