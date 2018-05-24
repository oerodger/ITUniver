﻿using Docflow.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Docflow.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString GetCurrentUser(this HtmlHelper html)
        {
            var controller = html.ViewContext.Controller as BaseController;
            if (controller != null)
            {
                return html.Partial("DisplayTemplates/User", controller.CurrentUser);
            }
            return MvcHtmlString.Empty;
        }
    }
}