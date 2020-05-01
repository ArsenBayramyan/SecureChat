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
        private SecureChat.DAL.User _user;
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
                };
                listuser.Add(userpl);
            }
            var users = listuser.Where(u => u.Id != currentUseer.Id).ToList();
            var messageBL = new MessageBL(_uow.messageRepository);
            _message.To = _message.To ?? string.Empty;
            if (_user.City=="1")
            {
                var user = ToUser(_user.Id);
                _user = new DAL.User
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
                    Email = user.Email
                };
            }
            var messages = messageBL.GetMessages(_message.From, _message.To);
            var chatModel = new ChatViewModel
            {
                Users = users,
                Messages = messages,
                CurrentUser = currentUseer,
                ToUser =new User
                {
                    Id = _user.Id,
                    UserName = _user.Email,
                    FirstName = _user.FirstName??string.Empty,
                    LastName = _user.LastName??string.Empty,
                    BirthDate = _user.BirthDate,
                    Sex = _user.Sex??string.Empty,
                    City = _user.City,
                    Address = _user.Address,
                    PasswordHash = _user.PasswordHash,
                    Email = _user.Email
                }
            };
            return View(chatModel);
        }
        [HttpGet]
        public ActionResult GetMessagedByUser([FromRoute]string Id)
        {
            _message.To = Id;
            _user.Id = Id;
            _user.City = "1";
            return RedirectToAction("Index");
        }
        public ActionResult DeleteMessage(string Id)
        {
            MessageBL messageBL = new MessageBL(_uow.messageRepository);
            messageBL.DeleteById(Id);
            return RedirectToAction("Index");
        }
    }
}
