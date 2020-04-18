

using System;

namespace SecureChat.Core.Interfaces
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
        string City { get; set; }
        string Address { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        DateTime RegistretionDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
