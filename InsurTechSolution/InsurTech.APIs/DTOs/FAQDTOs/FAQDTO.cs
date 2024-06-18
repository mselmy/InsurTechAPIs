using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.FAQDTOs
{
    public class FAQDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage = "Answer must be at least 3 characters")]
        public string Answer { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Body must be at least 3 characters")]
        public string Body { get; set; }

        [Required]
        public string UserId { get; set; }


    }
}
