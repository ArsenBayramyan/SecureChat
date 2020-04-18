using Microsoft.AspNetCore.Identity;
using SecureChat.BLL.Models;
using SecureChat.BLL.Repository;
using SecureChat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureChat.BLL.BL
{
    public class UserBL : BaseBL<UserRepository>
    {
        public UserBL(UserRepository repository):base(repository)
        {}

        public bool Delete(IUser entity) => repository.Delete(entity);
        public bool DeleteById(int id) => repository.DeleteById(id);
        public IUser GetByID(int id) => repository.GetByID(id);
        public IEnumerable<IUser> List() => repository.List();

        public bool Update(IUser entity) => repository.Update(entity);
        public bool SaveUser(IUser entity)
        {
            if (true)
            {
                entity.RegistretionDate = DateTime.Now;

                return repository.Save(entity);
            }
            return false;
        }
        public bool LoginUser(IUser userLogin,Task<IUser> user)
        {

            if (user!=null)
            {
               
            }
            return false;
        }

        public void SetSigninManager(SignInManager<User> signInManager)
        {

        }
    }
}
