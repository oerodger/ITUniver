using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models
{
    public class Folder
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime? ChangeDate { get; set; }
        public virtual User CreationAuthor { get; set; }
        public virtual User ChangeAuthor { get; set; }
        public virtual Folder ParentFolder { get; set; }
    }
}
