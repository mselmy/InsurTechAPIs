using InsurTech.APIs.CustomeValidation;
using InsurTech.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.DTOs.HomeInsurancePlanDTO
{
    public class EditHomeInsuranceDTO
    {
        public int Id { get; set; }


        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Yearly Coverage must be a non-negative number.")]
        public decimal YearlyCoverage { get; set; }

        [Required]
        [JsonConverter(typeof(InsurancePlanLevelConverter))]
        [CheckLevel]
        public InsurancePlanLevel Level { get; set; }

        [JsonIgnore]
        [CheckCategoury]
        public int CategoryId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Quotation must be a non-negative number.")]
        public decimal Quotation { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Company ID must be between 1 and 100 characters.")]
        public string CompanyId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Water Damage must be a non-negative number.")]
        public decimal WaterDamage { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Glass Breakage must be a non-negative number.")]
        public decimal GlassBreakage { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Natural Hazard must be a non-negative number.")]
        public decimal NaturalHazard { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Attempt theft must be a non-negative number.")]
        public decimal AttemptedTheft { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "fires and explosion must be a non-negative number.")]
        public decimal FiresAndExplosion { get; set; }
        public EditHomeInsuranceDTO()
        {
            CategoryId = 2;
        }
    }
}
