using InsurTech.APIs.CustomeValidation;
using InsurTech.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.CompanyUpdateDto
{
	public class CompanyUpdateDto
	{
		[Required]
		public string Id { get; set; }

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
		[RegularExpression(@"^(\d{9})$", ErrorMessage = "Invalid Tax Number")]

		public string TaxNumber { get; set; }
		[Required]
		public string Location { get; set; }

		[Required]
		[RegularExpression(@"^01(0|1|2|5)[0-9]{8}$")]
		public string phoneNumber { get; set; }
	}
}
