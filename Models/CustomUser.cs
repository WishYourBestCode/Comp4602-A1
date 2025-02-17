using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace Blog.Models;

 public class CustomUser : IdentityUser
    {
        public CustomUser() : base() { }

        [Required]
        public string? FirstName { get; set; }
        
        [Required]
        public string? LastName { get; set; }

        public bool IsApproved { get; set; } = false;
    }