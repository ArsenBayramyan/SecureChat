using Microsoft.AspNetCore.Identity;
using SecureChat.Core.Interfaces;
using System;

namespace SecureChat.BLL.Models
{
    public class User :IdentityUser,IUser
    {
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
