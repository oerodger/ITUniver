using Docflow.Auth;
using Docflow.Models;
using Docflow.Models.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docflow.Controllers
{
    public class BaseController : Controller
    {
        protected UserRepository userRepository;

        public BaseController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().Get<SignInManager>(); }
        }

        public UserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
        }

        public User CurrentUser
        {
            get
            {
                if (User == null || User.Identity == null)
                {
                    return null;
                }
                var currentUserId = User.Identity.GetUserId();
                long userId;
                if (string.IsNullOrEmpty(currentUserId) || !long.TryParse(currentUserId, out userId))
                {
                    return null;
                }
                return userRepository.Load(userId);
            }
        }
    }
}