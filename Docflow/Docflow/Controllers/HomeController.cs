using Docflow.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docflow.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserRepository userRepository) : 
            base(userRepository)
        {
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}