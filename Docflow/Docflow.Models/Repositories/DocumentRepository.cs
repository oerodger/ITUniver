using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Docflow.Models.Repositories
{
    [Repository]
    public class DocumentRepository : Repository<Document>
    {
        public DocumentRepository(ISession session) : 
            base(session)
        {
        }
    }
}
