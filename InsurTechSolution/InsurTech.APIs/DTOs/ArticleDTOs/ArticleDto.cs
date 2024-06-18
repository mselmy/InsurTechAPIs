using AutoMapper;
using InsurTech.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.ArticleDTOs
{
    public class ArticleDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        public string Title { get; set; }
        [Required]
        [MinLength(50, ErrorMessage = "Content must be at least 50 characters")]
        public string Content { get; set; }
        [Required]
        public string ArticleImg { get; set; }
    }
}
