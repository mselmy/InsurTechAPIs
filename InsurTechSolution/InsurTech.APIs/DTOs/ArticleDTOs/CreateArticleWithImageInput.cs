using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.ArticleDTOs
{
    public class CreateArticleWithImageInput
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        public string Title { get; set; }
        [Required]
        [MinLength(50, ErrorMessage = "Content must be at least 50 characters")]
        public string Content { get; set; }
        [Required]
        public IFormFile File { get; set;}
    }
}
