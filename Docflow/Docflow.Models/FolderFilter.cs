using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models
{
    public class FolderFilter
    {
        public string Name { get; set; }

        public RangeDateTime CreationDate { get; set; }

        public FolderFilter()
        {
            CreationDate = new RangeDateTime();
        }
    }
}
