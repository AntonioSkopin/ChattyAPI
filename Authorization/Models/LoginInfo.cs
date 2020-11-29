using System.ComponentModel.DataAnnotations;

namespace ChattyAPI.Authorization.Models
{
    public class LoginInfo
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string username { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 3)]
        public string password { get; set; }
    }
}