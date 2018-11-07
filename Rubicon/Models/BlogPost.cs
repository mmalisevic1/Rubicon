using System;
using System.ComponentModel.DataAnnotations;

namespace Rubicon.Models
{
    public class BlogPost
    {
        [Required]
        public string Slug { get; set; }

        [MaxLength(300, ErrorMessage = "Title can contain 300 characters max.")]
        [Required]
        public string Title { get; set; }

        [MaxLength(1000, ErrorMessage = "Description can contain 1000 characters max.")]
        [Required]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        public string[] TagList { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
