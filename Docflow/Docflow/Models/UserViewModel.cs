using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Docflow.Models
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}