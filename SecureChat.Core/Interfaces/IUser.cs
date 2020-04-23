

using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecureChat.Core.Interfaces
{
    public interface IUser
    {
        
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
        string City { get; set; }
        string Address { get; set; }
        string ConfirmPassword { get; set; }
        DateTime RegistrationDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
