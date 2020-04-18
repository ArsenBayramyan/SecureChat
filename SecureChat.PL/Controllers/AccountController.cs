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
        private SignInManager<User> _managerMgr;
        public AccountController(IRepository<User> repository,SignInManager<User> managerMgr)
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
                var user = new User
                {
                    FirstName = createUser.FirstName,
                    LastName = createUser.LastName,
                    BirthDate = createUser.BirthDate,
                    City = createUser.City,
                    Address = createUser.Address,
                    Password = createUser.Password,
                    ConfirmPassword = createUser.ConfirmPassword,
                    Email=createUser.Email,
                };

                if (userBL.SaveUser(user))
                {
                    return RedirectToAction("Login",new UserLoginModel
                                                        { Email=user.Email,
                                                          Password =user.Password
                                                         });
                }    
            }

            return View(createUser);
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(UserLoginModel userLogin)
        {
            UserBL userBl = new UserBL(_repository);
            if (ModelState.IsValid)
            {
                IUser user = new User
                {
                    Email=userLogin.Email,
                    Password=userLogin.Password
                };
                IUser FindUser = _repository.GetByEmail(user);
                
                userBl.LoginUser(user,FindUser);
            }
            return null;
        }

        [HttpPost]
        public bool Delete(User userDelete)
        {
            var entity = userDelete as IUser;

            return _repository.Delete(entity);
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
