using InsurTech.Core.Entities;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.DTOs.InsurancePlanDTO
{
	public class InsurancePlanDTO
	{
		public int Id { get; set; }
		public decimal YearlyCoverage { get; set; }
		public InsurancePlanLevel Level { get; set; }
		public decimal Quotation { get; set; }
		public string Company { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? WaterDamage { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? GlassBreakage { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? NaturalHazard { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? AttemptedTheft { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? FiresAndExplosion { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string MedicalNetwork { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? ClinicsCoverage { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? HospitalizationAndSurgery { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? OpticalCoverage { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? DentalCoverage { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? PersonalAccident { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? Theft { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? ThirdPartyLiability { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? OwnDamage { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? LegalExpenses { get; set; }
	}
}
