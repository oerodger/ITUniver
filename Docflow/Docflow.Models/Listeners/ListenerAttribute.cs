﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Listeners
{
    public class ListenerAttribute: Attribute
    {
        public ListenerType ListenerType { get; set; }
    }
}
