using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureChat.BLL.BL;
using SecureChat.BLL.Repository;
using SecureChat.Core.Interfaces;
using SecureChat.PL.Models;
using System.Net;
using System.Threading.Tasks;


namespace SecureChat.PL.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository _repository;
        private SignInManager<SecureChat.DAL.User> _managerMgr;
        public AccountController(IRepository<SecureChat.DAL.User> repository,SignInManager<SecureChat.DAL.User> managerMgr)
        {
            _repository = repository as UserRepository;
            _managerMgr = managerMgr;
        }
        [HttpGet]
        public ViewResult Registration() => View();

        [HttpPost]
        public IActionResult Registration(UserCreateModel createUser)
        {
            var userBL = new UserBL(_repository);

            if (ModelState.IsValid)
            {
                var user = new SecureChat.BLL.Models.User
                {
                    UserName = createUser.FirstName,
                    LastName = createUser.LastName,
                    BirthDate = createUser.BirthDate,
                    City = createUser.City,
                    Address = createUser.Address,
                    PasswordHash = createUser.Password,
                    ConfirmPassword = createUser.ConfirmPassword,
                    Email=createUser.Email,
                };

                if (userBL.SaveUser(user))
                {
                    return RedirectToAction("Login", "Account", null);
                }    
            }
            return View(createUser);
        }
        
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLoginModel userLogin)
        {
            UserBL userBl = new UserBL(_repository);
            if (ModelState.IsValid)
            {
                var user = new SecureChat.DAL.User
                {
                    Email = userLogin.Email,
                    PasswordHash = userLogin.Password,
                };
                var FindUser = _repository.GetByEmail(user) as SecureChat.DAL.User;
                if (FindUser != null)
                {
                    _managerMgr.SignOutAsync();
                    var result =
                          _managerMgr.PasswordSignInAsync(FindUser, user.PasswordHash, false, false).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("/Home/Index");
                    }
                }
                ModelState.AddModelError(nameof(userLogin.Email), "Invalid user or password");
            }
            return RedirectToAction("LoginPage");
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
