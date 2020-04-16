using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureChat.DAL;
using SecureChat.PL.Models;

namespace SecureChat.PL.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        public AccountController(UserManager<User> users)
        {
            userManager = users;
        }
        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel createUser)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = createUser.Name,
                    LastName = createUser.LastName,
                    DateOfBirth = createUser.DateOfBirth,
                    City = createUser.City,
                    Address = createUser.Address,
                    Password = createUser.Password,
                    ConfirmPassword = createUser.ConfirmPassword
                };
                IdentityResult result = await userManager.CreateAsync(user, createUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(createUser);
        }
    }
}
