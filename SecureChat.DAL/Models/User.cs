using Microsoft.AspNetCore.Identity;
using SecureChat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecureChat.DAL
{
    public class User:IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime RegistretionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
