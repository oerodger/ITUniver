﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Docflow.Models
{
    public class FetchOptions
    {
        public string SortExpression { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
