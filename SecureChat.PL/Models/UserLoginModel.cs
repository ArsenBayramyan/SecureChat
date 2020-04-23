
using System.ComponentModel.DataAnnotations;

namespace SecureChat.PL.Models
{
    public class UserLoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
