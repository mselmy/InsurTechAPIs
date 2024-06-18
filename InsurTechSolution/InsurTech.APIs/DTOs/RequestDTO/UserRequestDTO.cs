using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.RequestDTO
{
	public class UserRequestDTO
	{
		public string CustomerName { get; set; }
		public string InsurancePlanLevel { get; set; }
		public decimal YearlyCoverage { get; set; }
		public decimal Quotation { get; set; }
		public string Status { get; set; }
	}
}
