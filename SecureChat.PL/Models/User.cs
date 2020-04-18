using SecureChat.Core.Interfaces;
using System;

namespace SecureChat.PL.Models
{
    public class User : IUser
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
