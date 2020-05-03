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
        private UnitOfWorkRepository _uow;
        private IMessage _message;
        private IUser _user;
        public ChatController(UserManager<SecureChat.DAL.User> userManager, MessagesDBContext context,
            SignInManager<SecureChat.DAL.User> signInManager,IMessage message, IUser user)
        {
            _uow = new UnitOfWorkRepository(userManager, context,signInManager);
            _message = message;
            _user = user as SecureChat.DAL.User;
        }
        public IActionResult Login() => View();
       
        public User CurrentUser()
        {
            var user = _uow.userRepository.GetUserByName(HttpContext.User.Identity.Name);
            return new User
            {
                Id=user.Id,
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Sex=user.Sex,
                City = user.City,
                Address = user.Address,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                IsDeleted=user.IsDeleted
            };
        }
        public User ToUser(string Id)
        {
            var user = _uow.userRepository.GetByID(Id);
            return new User
            {
                Id = user.Id,
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Sex = user.Sex,
                City = user.City,
                Address = user.Address,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                IsDeleted=user.IsDeleted
            };

        }
        [HttpPost]
        public ActionResult SendMessage([FromForm]string MessageBody)
        {
            _message.Body = MessageBody;
            var messageBL = new MessageBL(_uow.messageRepository);
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
            var list = _uow.userRepository.List().ToList();
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
                    Sex=user.Sex,
                    City = user.City,
                    Address = user.Address,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                    IsDeleted=user.IsDeleted
                };
                listuser.Add(userpl);
            }
            var users = listuser.Where(u => u.Id != currentUseer.Id && u.IsDeleted==false).ToList();
            var messageBL = new MessageBL(_uow.messageRepository);
            _message.To = _message.To ?? string.Empty;
            if (_message.To!=string.Empty)
            {
                _user = ToUser(_message.To);
            };
            var messages = messageBL.GetMessages(_message.From, _message.To);
            var chatModel = new ChatViewModel
            {
                Users = users,
                Messages = messages,
                CurrentUser = currentUseer,
                ToUser = _user
            };
            return View(chatModel);
        }
        [HttpGet]
        public ActionResult GetMessagesByUser([FromRoute]string Id)
        {
            _message.To = Id;
            return RedirectToAction("Index");
        }
        public ActionResult DeleteMessage(string Id)
        {
            MessageBL messageBL = new MessageBL(_uow.messageRepository);
            messageBL.DeleteById(Id);
            return RedirectToAction("Index");
        }
        public ActionResult Messages()
        {
            var messages=Json(_uow.messageRepository.List());
            return messages;
        }
    }
}
