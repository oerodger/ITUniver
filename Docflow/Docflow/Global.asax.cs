using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Docflow
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            if (ex is NullReferenceException)
            {
                Server.Transfer("Error.cshtml");
            }
        }
    }
}
