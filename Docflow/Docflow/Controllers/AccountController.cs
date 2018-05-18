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

            }
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}