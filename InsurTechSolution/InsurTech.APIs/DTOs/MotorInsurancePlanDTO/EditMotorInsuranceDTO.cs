using InsurTech.APIs.CustomeValidation;
using InsurTech.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.DTOs.MotorInsurancePlanDTO
{
    public class EditMotorInsuranceDTO
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
        [Range(0, double.MaxValue, ErrorMessage = "Personal Accident must be a non-negative number.")]
        public decimal PersonalAccident { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Theft must be a non-negative number.")]
        public decimal Theft { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Third Party Liability must be a non-negative number.")]
        public decimal ThirdPartyLiability { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Own Damage must be a non-negative number.")]
        public decimal OwnDamage { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Legal Expenses must be a non-negative number.")]
        public decimal LegalExpenses { get; set; }

        public EditMotorInsuranceDTO()
        {
            CategoryId = 3;
        }
    }
}
