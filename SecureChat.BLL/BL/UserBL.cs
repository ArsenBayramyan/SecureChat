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

        public bool Delete(User entity)
        {
            var _entity = new SecureChat.DAL.User
            {
                UserName = entity.Email,
                FirstName=entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                City = entity.City,
                Address = entity.Address,
                PasswordHash = entity.PasswordHash,
                Email = entity.Email,
            };
            return repository.Delete(_entity);
        }
        public bool DeleteById(string id) => repository.DeleteById(id);
        public User GetByID(string id)
        {
            var user=repository.GetByID(id);
            var _entity = new SecureChat.BLL.Models.User
            {
                UserName = user.Email,
                FirstName=user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                City = user.City,
                Address = user.Address,
                PasswordHash =user.PasswordHash,
                Email =user.Email,
            };
            return _entity;
        }
        public IEnumerable<SecureChat.DAL.User> List()
        {
            return repository.List();
        }
        public bool Update(User entity)
        {
            var _entity = new SecureChat.DAL.User
            {
                UserName = entity.Email,
                FirstName=entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                City = entity.City,
                Address = entity.Address,
                PasswordHash = entity.PasswordHash,
                Email = entity.Email,
            };
            return repository.Update(_entity);
        }
        public bool SaveUser(User entity)
        {
            var _entity = new SecureChat.DAL.User
            {
                UserName=entity.Email,
                FirstName=entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                City = entity.City,
                Address = entity.Address,
                PasswordHash = entity.PasswordHash,
                Email = entity.Email,
            };
            var resultUser = repository.GetByEmail(_entity);
            if (resultUser == null)
            {
                entity.RegistrationDate = DateTime.Now;
                return repository.Save(_entity);
            }
            return false;
        }
        public IUser GetUserByName(string name) => repository.GetUserByName(name);
    }
}
