using InsurTech.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.FeedbackDTOs
{
    public class CreateFeedbackInput
    {
        [Required]
        public int InsurancePlanId { get; set; } // This should be a simple type like int, no complex types or navigation properties

        [Required]
        public Rating Rating { get; set; } // Enum, should be safe

        [Required]
        [MaxLength(1000, ErrorMessage = "Comment must be at most 1000 characters")]
        public string Comment { get; set; } // Simple string property
    }
}
