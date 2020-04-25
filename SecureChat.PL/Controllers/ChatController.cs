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

namespace SecureChat.PL.Controllers
{
    [Authorize]
    public class ChatController:Controller
    {
        //private MessageRepository repository;
        //private UserRepository userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UnitOfWorkRepository uow;
        private UserManager<SecureChat.DAL.User> userManager;
        public ChatController(UserManager<SecureChat.DAL.User> userManager, MessagesDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            uow = new UnitOfWorkRepository(userManager, context);
            _httpContextAccessor = httpContextAccessor;
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
        [HttpGet]
        public IUser GetSenderUser()
        {
            UserBL userBL = new UserBL(uow.userRepository);
            return userBL.GetUserByName(HttpContext.User.Identity.Name);
        }
        [HttpPost]
        public bool SentMessage()
        {
            Message messageView = new Message();
            var to = RouteData?.Values["user"].ToString();
            UserBL userBL = new UserBL(uow.userRepository);
            //messageView.From = GetSenderUser().FirstName;
            //messageView.To = userBL.GetUserByName(to).UserID;
            messageView.Body = null;
            MessageBL messageBL = new MessageBL(uow.messageRepository);
            var messageB = new SecureChat.BLL.Models.Message
            {
                From = messageView.From,
                To = messageView.To,
                Body = messageView.Body
            };
            return messageBL.SendMessage(messageB);
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var chatViewModel = new ChatViewModel();
            var list = uow.userRepository.List().ToList();
            var listuser = new List<SecureChat.PL.Models.User>();
            foreach (var user in list)
            {
                var userpl = new SecureChat.PL.Models.User
                {
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
            chatViewModel.Users = listuser;
            var lisUser = listuser.Where(u => u.UserName ==);

            //  chatViewModel.Messages = uow.messageRepository.List().ToList();
            return View(chatViewModel);
        }
    }
}
