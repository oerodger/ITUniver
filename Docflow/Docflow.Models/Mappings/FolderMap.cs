using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Mappings
{
    public class FolderMap: ClassMap<Folder>
    {
        public FolderMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Name).Length(300);
            Map(f => f.CreationDate);
            References(f => f.CreationAuthor);
            Map(f => f.ChangeDate);
            References(f => f.ChangeAuthor);
            References(f => f.ParentFolder);
        }
    }
}
