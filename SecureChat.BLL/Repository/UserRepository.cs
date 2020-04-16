using Microsoft.AspNetCore.Identity;
using SecureChat.Core.Interfaces;
using SecureChat.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.BLL.Repository
{
    public class UserRepository : IRepository<IUser>
    {
        private UserManager<IUser> _userManager;

        public UserRepository(UserManager<IUser> userManager)
        {
            this._userManager = userManager;
        }

        public bool Delete(IUser entity)
        {
            return _userManager.DeleteAsync(entity).Result.Succeeded;
        }

        public bool DeleteById(int id)
        {
            var entity = _userManager.FindByIdAsync(id.ToString()).Result;

            return _userManager.DeleteAsync(entity).Result.Succeeded;
        }

        public IUser GetByID(int id)
        {
            return _userManager.FindByIdAsync(id.ToString()).Result;
        }

        public IEnumerable<IUser> List()
        {
           return _userManager.Users;
        }

        public bool Save(IUser entity)
        {
            return _userManager.CreateAsync(entity).Result.Succeeded;
        }

        public bool Update(IUser entity)
        {
           return _userManager.UpdateAsync(entity).Result.Succeeded;
        }
    }
}
