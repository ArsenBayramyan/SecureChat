using Microsoft.AspNetCore.Identity;
using SecureChat.Core.Interfaces;
using SecureChat.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecureChat.BLL.Repository
{
    public class UserRepository : IRepository<User>
    {
        private UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }
        public bool Delete(User entity)
        {
            return _userManager.DeleteAsync(entity).Result.Succeeded;
        }

        public bool DeleteById(int id)
        {
            var entity = _userManager.FindByIdAsync(id.ToString()).Result;

            return _userManager.DeleteAsync(entity).Result.Succeeded;
        }

        public User GetByID(int id)
        {
            return _userManager.FindByIdAsync(id.ToString()).Result;
        }

        public IEnumerable<User> List()
        {
            var x = _userManager.Users;
           return _userManager.Users;
        }

        public bool Save(User entity)
        {
            var result = _userManager.CreateAsync(entity,entity.PasswordHash).Result;
            
            return result.Succeeded;
        }

        public bool Update(User entity)
        {
              return _userManager.UpdateAsync(entity).Result.Succeeded;
           
        }
        public User GetByEmail(User _user)
        {
            return _userManager.FindByEmailAsync(_user.Email).Result;
        }
        public  User GetUserByName(string name)
        {
            return _userManager.FindByNameAsync(name).Result;
        }
    }
}
