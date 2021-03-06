﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models
{
    public class Document : Folder
    {
        public Document()
        {
            Versions = new List<DocumentVersion>();
        }

        public virtual string Description { get; set; }
        public virtual IList<DocumentVersion> Versions { get; set;}
    }
}
