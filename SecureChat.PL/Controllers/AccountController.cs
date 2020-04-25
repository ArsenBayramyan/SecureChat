using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SecureChat.BLL.BL;
using SecureChat.BLL.Repository;
using SecureChat.Core;
using SecureChat.Core.Interfaces;
using SecureChat.PL.Models;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;


namespace SecureChat.PL.Controllers
{
   // [Authorize]
    public class AccountController : Controller
    {
        private UserRepository _repository;
        private SignInManager<SecureChat.DAL.User> _managerMgr;
        private UserManager<SecureChat.DAL.User> _userManager;
        public AccountController(IRepository<SecureChat.DAL.User> repository,SignInManager<SecureChat.DAL.User> managerMgr, UserManager<SecureChat.DAL.User> userManager)
        {
            _repository = repository as UserRepository;
            _managerMgr = managerMgr;
            _userManager = userManager;
        }
        [HttpGet]
        public ViewResult Registration() => View();

        [HttpPost]
       // [AllowAnonymous]
        public IActionResult Registration(UserCreateModel createUser)
        {
            var userBL = new UserBL(_repository);
            
            if (ModelState.IsValid)
            {
                var user = new SecureChat.BLL.Models.User
                {
                    UserName = createUser.Email,
                    FirstName=createUser.FirstName,
                    LastName = createUser.LastName,
                    BirthDate = createUser.BirthDate,
                    City = createUser.City,
                    Address = createUser.Address,
                    PasswordHash = createUser.Password,
                    Email=createUser.Email,
                };
                
                if (userBL.SaveUser(user))
                {
                    return RedirectToAction("Login",new UserLoginModel
                                                        {
                                                           Email=user.Email,
                                                           Password=user.PasswordHash
                                                        });
                }
                   
            }
            return View(createUser);
        }
        [HttpGet]
       // [AllowAnonymous]
        public ViewResult Login() => View();

        [HttpPost]
       // [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userLogin)
        {
            UserBL userBl = new UserBL(_repository);
            if (ModelState.IsValid)
            {
                var user = new SecureChat.DAL.User
                {
                    Email = userLogin.Email,
                    PasswordHash = userLogin.Password,
                };
                var FindUser = _repository.GetByEmail(user);
                if (FindUser != null)
                {
                   await _managerMgr.SignOutAsync();
                    var result =
                          _managerMgr.PasswordSignInAsync(FindUser, user.PasswordHash, true, false).Result;
                    if (result.Succeeded)
                    {
                        Singleton.getInstance(FindUser.UserName);
                        return RedirectToAction("/Chat/Index");
                    }
                }
                ModelState.AddModelError(nameof(userLogin.Email), "Invalid user or password");
            }
            return RedirectToAction("Login");
        }
        public IActionResult LogOut() => RedirectToAction("Login");
       
        [HttpPost]
        public bool Delete(User userDelete)
        {
            var entity = userDelete as IUser;

            return _repository.Delete(entity as SecureChat.DAL.User);
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
