using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models
{
    public class File
    {
        public virtual long Id { get; set; }
        public virtual byte[] Content { get; set; }
    }
}
