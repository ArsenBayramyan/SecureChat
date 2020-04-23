﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureChat.BLL.BL;
using SecureChat.BLL.Models;
using SecureChat.BLL.Repository;
using SecureChat.Core.Interfaces;
using SecureChat.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Message = SecureChat.PL.Models.Message;

namespace SecureChat.PL.Controllers
{
    public class ChatController:Controller
    {
        private MessageRepository repository;
       // private UserRepository userRepository;
        public ChatController(IRepository<IMessage> _repository)
        {
            this.repository = _repository as MessageRepository;
           // this.userRepository = userrepo;

        }
        public IActionResult Login() => View();
        [HttpPost]
        public bool Delete(Message message)
        {
            MessageBL messageBL = new MessageBL(repository);
            var messageB = new SecureChat.BLL.Models.Message {
                From = message.From,
                To = message.To,
                Body = message.Body,
                IsDeleted = true
            };
            return messageBL.Delete(messageB);
        }
        //[HttpGet]
        //public IUser GetSenderUser()
        //{
        //    UserBL userBL = new UserBL(userRepository);
        //    return userBL.GetUserByName(HttpContext.User.Identity.Name);
        //}
        //[HttpPost]
        //public bool SentMessage()
        //{
        //    Message messageView = new Message();
        //    var to = RouteData?.Values["user"].ToString();
        //    UserBL userBL = new UserBL(userRepository);
        //    messageView.From = GetSenderUser().UserID;
        //    messageView.To = userBL.GetUserByName(to).UserID;
        //    messageView.Body = null;
        //    MessageBL messageBL = new MessageBL(repository);
        //    var messageB = new SecureChat.BLL.Models.Message
        //    {
        //        From = messageView.From,
        //        To = messageView.To,
        //        Body = messageView.Body
        //    };
        //    return messageBL.SendMessage(messageB);
        //}

    }
}
