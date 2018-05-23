using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Mappings
{
    public class DocumentVersionMap: ClassMap<DocumentVersion>
    {
        public DocumentVersionMap()
        {
            Id(v => v.Id);
            Map(f => f.CreationDate);
            References(f => f.CreationAuthor);
            Map(f => f.ChangeDate);
            References(f => f.ChangeAuthor);
            References(f => f.File);
            References(f => f.Document).Column("Document");
        }
    }
}
