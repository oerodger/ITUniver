using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Mappings
{
    public class FileMap: ClassMap<File>
    {
        public FileMap()
        {
            Id(f => f.Id);
            Map(f => f.Content).Length(int.MaxValue);
        }
    }
}
