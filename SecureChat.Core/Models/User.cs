using Microsoft.AspNetCore.Identity;
using SecureChat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.Core.Models
{
    public class User : IdentityUser, IUser
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
