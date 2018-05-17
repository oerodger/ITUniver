using Docflow.Models;
using Docflow.Models.Repositories;
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

        public AccountController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult CreateUser(string login, string password)
        {
            var user = new User
            {
                Login = login,
                Password = password,
                Name = "admin"
            };
            userRepository.Save(user);
            return View("Index");
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}