using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using SecureChat.BLL.BL;
using SecureChat.BLL.Repository;
using SecureChat.Core;
using SecureChat.Core.Interfaces;
using SecureChat.DAL;
using SecureChat.DAL.Contexts;
using SecureChat.PL.Models;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using User = SecureChat.DAL.User;

namespace SecureChat.PL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UnitOfWorkRepository _uow;
        public AccountController(UserManager<SecureChat.DAL.User> userManager, MessagesDBContext context,
            SignInManager<SecureChat.DAL.User> signInManager)
        {
            _uow = new UnitOfWorkRepository(userManager, context, signInManager);
        }
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Registration() => View();

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Registration(UserCreateModel createUser)
        {
            var userBL = new UserBL(_uow.userRepository);

            if (ModelState.IsValid)
            {
                var user = new SecureChat.BLL.Models.User
                {
                    UserName = createUser.Email,
                    FirstName = createUser.FirstName,
                    LastName = createUser.LastName,
                    BirthDate = createUser.BirthDate,
                    Sex = createUser.Sex,
                    City = createUser.City,
                    Address = createUser.Address,
                    PasswordHash = createUser.Password,
                    Email = createUser.Email,
                };

                if (userBL.SaveUser(user))
                {
                    return RedirectToAction("Login", new UserLoginModel
                    {
                        Email = user.Email,
                        Password = user.PasswordHash
                    });
                }

            }
            return View(createUser);
        }
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userLogin)
        {
            UserBL userBl = new UserBL(_uow.userRepository);
            if (ModelState.IsValid)
            {
                var user = new SecureChat.DAL.User
                {
                    Email = userLogin.Email,
                    PasswordHash = userLogin.Password,
                };
                var FindUser = _uow.userRepository.GetByEmail(user);
                if (FindUser != null && FindUser.IsDeleted == false)
                {
                    await _uow.signInManager.SignOutAsync();
                    var result =
                          _uow.signInManager.PasswordSignInAsync(FindUser, user.PasswordHash, true, false).Result;
                    if (result.Succeeded)
                    {
                        return Redirect("/Chat/Index");
                    }
                }
                ModelState.AddModelError(nameof(userLogin.Email), "Invalid user or password");
            }
            return RedirectToAction("Login");
        }
        public IActionResult LogOut() => RedirectToAction("Login");

        [HttpPost]
        public ActionResult Delete(string Id)
        {
            UserBL userBl = new UserBL(_uow.userRepository);
            userBl.DeleteById(Id);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return null;
        }

        [HttpGet]
        public IActionResult GetUser(string s)
        {
            return null;
        }

        [HttpPost]
        public IActionResult Update()
        {
            return null;
        }

    }
}