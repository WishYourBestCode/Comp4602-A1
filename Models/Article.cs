using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        public string? Title { get; set; } // short text

        [Required]
        public string? Body { get; set; }  // HTML allowed

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // The email (or user ID) of the contributor who owns this article
        [Required]
        public string? ContributorUsername { get; set; }
    }
}
