using InsurTech.APIs.CustomeValidation;
using InsurTech.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.DTOs.HealthInsurancePlanDTO
{
    public class EditHealthInsuranceDTO
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
        public string MedicalNetwork { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Clinical Coverage must be a non-negative number.")]
        public decimal ClinicsCoverage { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Hospitalization and surgery must be a non-negative number.")]
        public decimal HospitalizationAndSurgery { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "optical coverage must be a non-negative number.")]
        public decimal OpticalCoverage { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Dental coverage must be a non-negative number.")]
        public decimal DentalCoverage { get; set; }
        public EditHealthInsuranceDTO()
        {
            CategoryId = 1;
        }
    }
}
