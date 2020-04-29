using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureChat.BLL.BL;
using SecureChat.BLL.Models;
using SecureChat.BLL.Repository;
using SecureChat.Core;
using SecureChat.Core.Interfaces;
using SecureChat.DAL.Contexts;
using SecureChat.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Message = SecureChat.PL.Models.Message;
using User = SecureChat.PL.Models.User;

namespace SecureChat.PL.Controllers
{
    [Authorize]
    public class ChatController:Controller
    {
        private UnitOfWorkRepository uow;
        private UserManager<SecureChat.DAL.User> userManager;
        private IMessage _message;
        public ChatController(UserManager<SecureChat.DAL.User> userManager, MessagesDBContext context,IMessage message)
        {
            this.userManager = userManager;
            uow = new UnitOfWorkRepository(userManager, context);
            _message = message;
        }
        public IActionResult Login() => View();
        [HttpPost]
        public bool Delete(Message message)
        {
            MessageBL messageBL = new MessageBL(uow.messageRepository);
            var messageB = new SecureChat.BLL.Models.Message {
                From = message.From,
                To = message.To,
                Body = message.Body,
                IsDeleted = true
            };
            return messageBL.Delete(messageB);
        }
        public User CurrentUser()
        {
            var user = uow.userRepository.GetUserByName(HttpContext.User.Identity.Name);
            return new User
            {
                Id=user.Id,
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                City = user.City,
                Address = user.Address,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
            };
        }
        [HttpPost]
        public ActionResult SendMessage([FromForm]string MessageBody)
        {
            _message.Body = MessageBody;
            var messageBL = new MessageBL(uow.messageRepository);
            messageBL.SendMessage(new SecureChat.BLL.Models.Message
            {
                MessageID = _message.MessageID,
                From = _message.From,
                To = _message.To,
                Body = _message.Body,
                SendDate = _message.SendDate,
                Status = _message.Status,
                IsDeleted = _message.IsDeleted
            });
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var currentUseer = CurrentUser();
            _message.From = currentUseer.Id;
            var chatViewModel = new ChatViewModel();
            var list = uow.userRepository.List().ToList();
            var listuser = new List<SecureChat.PL.Models.User>();
            foreach (var user in list)
            {
                var userpl = new SecureChat.PL.Models.User
                {
                    Id=user.Id,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    City = user.City,
                    Address = user.Address,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                };
                listuser.Add(userpl);
            }
            var users = listuser.Where(u => u.Id != currentUseer.Id).ToList();
            var messageBL = new MessageBL(uow.messageRepository);
            _message.To = _message.To ?? string.Empty;
            var messages = messageBL.GetMessages(_message.From, _message.To);
            var chatModel = new ChatViewModel
            {
                Users = users,
                Messages = messages,
                CurrentUser=currentUseer,
            };
            return View(chatModel);
        }
        [HttpGet]
        public ActionResult GetMessagedByUser([FromRoute]string Id)
        {
            _message.To = Id;
            return RedirectToAction("Index");
        }
        //[HttpPost]
        public ActionResult DeleteMessage(string Id)
        {
            MessageBL messageBL = new MessageBL(uow.messageRepository);
            messageBL.DeleteById(Id);
            return RedirectToAction("Index");
        }
    }
}
