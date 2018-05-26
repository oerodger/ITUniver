using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models
{
    public class RangeDateTime
    {
        [DataType(DataType.DateTime)]
        public DateTime? From { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? To { get; set; }
    }
}
