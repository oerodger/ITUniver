using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docflow.Extensions
{
    public class PopupViewModel
    {
        public PopupOptions Options { get; set; }

        public Func<HtmlHelper, MvcHtmlString> Render { get; set; }
    }
}