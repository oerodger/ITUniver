using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Mappings
{
    public class DocumentMap: SubclassMap<Document>
    {
        public DocumentMap()
        {
            Map(d => d.Description).Length(int.MaxValue);
            HasMany(d => d.Versions)
                .Cascade.SaveUpdate()
                .KeyColumn("Document");
        }
    }
}
