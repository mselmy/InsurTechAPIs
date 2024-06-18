using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.FAQDTOs
{
    public class CreateFAQInput
    {
        [Required]
        [MinLength(3, ErrorMessage = "Answer must be at least 3 characters")]
        public string Answer { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Body must be at least 3 characters")]
        public string Body { get; set; }
    }
}
