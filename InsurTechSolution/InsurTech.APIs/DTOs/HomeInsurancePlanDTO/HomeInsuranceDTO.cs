using InsurTech.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.HomeInsurancePlanDTO
{
    public class HomeInsuranceDTO
    {
        public int Id { get; set; }
        public decimal YearlyCoverage { get; set; }
        public InsurancePlanLevel Level { get; set; }
        public string Category { get; set; }
        public decimal Quotation { get; set; }
        public string Company { get; set; }
		public decimal WaterDamage { get; set; }
		public decimal GlassBreakage { get; set; }
		public decimal NaturalHazard { get; set; }
		public decimal AttemptedTheft { get; set; }
		public decimal FiresAndExplosion { get; set; }
		public int NumberOfUsers { get; set; }
    }
}
