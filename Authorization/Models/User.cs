using System;
using System.ComponentModel.DataAnnotations;

namespace ChattyAPI.Authorization.Models
{
    public class User
    {
        [Required]
        public Guid gd { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string username { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 5)]
        public string fullname { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 5)]
        public string email { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 3)]
        public string password { get; set; }

        [StringLength(75, MinimumLength = 1)]
        public string bio { get; set; }

        public DateTime birthdate { get; set; }
    }
}