using Docflow.Controllers;
using Docflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;

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

        public static MvcHtmlString SortLink(this HtmlHelper html,
            string linkText,
            string sortExpression,            
            string actionName, 
            string controllerName, RouteValueDictionary routeValues)
        {
            SortDirection? sort = null;
            var sortDirectionStr = html.ViewContext.HttpContext.Request["SortDirection"];
            if (!string.IsNullOrEmpty(sortDirectionStr) 
                && html.ViewContext.HttpContext.Request["SortExpression"] == sortExpression)
            {
                SortDirection s;
                if (Enum.TryParse(sortDirectionStr, out s))
                {
                    sort = s;
                }
            }            
            routeValues["SortExpression"] = sortExpression;
            routeValues["SortDirection"] = sort.HasValue && sort.Value == SortDirection.Ascending ?
                SortDirection.Descending : SortDirection.Ascending; 
            return html.Partial("SortLink", new SortLinkModel {
                ActionName = actionName,
                ControllerName = controllerName,
                SortDirection = sort,
                RouteValues = routeValues,
                LinkText = linkText
            });
        }
    }
}