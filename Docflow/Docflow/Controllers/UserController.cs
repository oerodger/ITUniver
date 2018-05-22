using Docflow.Models;
using Docflow.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docflow.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserRepository userRepository;

        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: User
        public ActionResult Index()
        {
            var model = new UserListViewModel
            {
                Users = userRepository.GetAll()
            };
            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var user = userRepository.Load(id);
            return View(new UserViewModel {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                UserStatus = user.UserStatus
            });
        }
    }
}