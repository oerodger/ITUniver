using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Docflow.Models
{
    public class FolderViewModel
    {
        public Folder Parent { get; set; }

        public Folder CurrentFolder { get; set; }

        public IList<Folder> Folders { get; set; }
    }
}