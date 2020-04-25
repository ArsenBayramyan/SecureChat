using Microsoft.AspNetCore.Identity;
using SecureChat.DAL;
using SecureChat.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.BLL.Repository
{
    public class UnitOfWorkRepository
    {
        public UserRepository userRepository;
        public MessageRepository messageRepository;
        public UnitOfWorkRepository(UserManager<User> userManager,MessagesDBContext context)
        {
            userRepository = new UserRepository(userManager);
            messageRepository = new MessageRepository(context);
        }
    }
}
