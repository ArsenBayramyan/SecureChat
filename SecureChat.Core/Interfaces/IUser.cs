using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecureChat.Core.Interfaces
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
        string Sex { get; set; }
        string City { get; set; }
        string Address { get; set; }
        DateTime RegistrationDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
