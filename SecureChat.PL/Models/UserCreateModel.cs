using System;
using System.ComponentModel.DataAnnotations;

namespace SecureChat.PL.Models
{
    public class UserCreateModel 
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/0:MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string City { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
