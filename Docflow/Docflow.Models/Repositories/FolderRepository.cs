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

        protected virtual void SetupFilter(ICriteria crit, FolderFilter filter)
        {
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    crit.Add(Restrictions.Like("Name", filter.Name, MatchMode.Anywhere));
                }
                if (filter.CreationDate != null)
                {
                    if (filter.CreationDate.From.HasValue)
                    {
                        crit.Add(Restrictions.Ge("CreationDate", filter.CreationDate.From.Value));
                    }
                    if (filter.CreationDate.To.HasValue)
                    {
                        crit.Add(Restrictions.Le("CreationDate", filter.CreationDate.To.Value));
                    }
                }
            }
        }

        public IList<Folder> GetFolders(long? id, FolderFilter filter = null, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Folder>();
            SetupFilter(crit, filter);
            if (id.HasValue)
            {
                crit = crit.Add(Restrictions.Eq("ParentFolder.Id", id.Value));
            }
            else
            {
                crit = crit.Add(Restrictions.IsNull("ParentFolder"));
            }
            if (options != null)
            {
                SetFetchOptions(crit, options);
            }
            return crit.List<Folder>();
        }
        
    }
}
