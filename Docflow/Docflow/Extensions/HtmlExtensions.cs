using Docflow.Controllers;
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
            if (controller != null && controller.CurrentUser != null)
            {
                return html.Partial("DisplayTemplates/User", controller.CurrentUser);
            }
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString Popup(this HtmlHelper html, PopupOptions options, Func<HtmlHelper, MvcHtmlString> render = null)
        {
            var popup = new PopupViewModel {
                Options = options,
                Render = render
            };
            return html.Partial("Popup", popup);
        }
    }
}