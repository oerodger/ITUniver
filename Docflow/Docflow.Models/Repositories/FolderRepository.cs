using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;

namespace Docflow.Models.Repositories
{
    [Repository]
    public class FolderRepository : Repository<Folder>
    {
        public FolderRepository(ISession session) : base(session)
        {
        }

        public IList<Folder> GetFolders(long? id)
        {
            var crit = session.CreateCriteria<Folder>();
            if (id.HasValue)
            {
                crit = crit.Add(Restrictions.Eq("ParentFolder.Id", id.Value));
            }
            else
            {
                crit = crit.Add(Restrictions.IsNull("ParentFolder"));
            }
            return crit.List<Folder>();
        }
    }
}
