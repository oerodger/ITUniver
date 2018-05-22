using Docflow.Auth;
using Docflow.Models;
using Docflow.Models.Repositories;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docflow.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository userRepository;

        public SignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().Get<SignInManager>(); }
        }

        public UserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
        }

        public AccountController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = SignInManager.PasswordSignIn(model.UserName, model.Password, false, false);
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateUser(string login, string password)
        {
            var user = new User { UserName = login };
            var result = UserManager.CreateAsync(user, password);
            if (result.Result.Succeeded)
            {
                SignInManager.SignIn(user, false, false);                
            }
            else
            {
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}