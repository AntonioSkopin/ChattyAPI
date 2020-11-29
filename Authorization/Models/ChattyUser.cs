using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ChattyAPI.Authorization.Models
{
    public class ChattyUser : IdentityUser
    {
        [Required]
        public Guid gd { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 5)]
        public string fullname { get; set; }

        [StringLength(75, MinimumLength = 1)]
        public string bio { get; set; }

        public DateTime birthdate { get; set; }
    }
}