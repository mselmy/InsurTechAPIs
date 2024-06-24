using InsurTech.APIs.CustomeValidation;
using InsurTech.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.DTOs.UserDTOs
{
	public class GetUserDTO
	{
		public string Id { get; set; }
		[Required]
		public UserType UserType { get; set; }
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
		public string Email { get; set; }
		[RegularExpression(@"^\d{14}$")]
		public string? NationalId { get; set; }
		//month should be less than 13 and day should be less than 32
		[RegularExpression(@"^(\d{4})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Invalid Date")]
		[CheckBirthDate]
		public string? BirthDate { get; set; }
		[RegularExpression(@"^01(0|1|2|5)[0-9]{8}$")]
		public string PhoneNumber { get; set; }
		[RegularExpression(@"^(\d{9})$", ErrorMessage = "Invalid Tax Number")]

		public string? TaxNumber { get; set; }
		public string? Location { get; set; }
	}
}
