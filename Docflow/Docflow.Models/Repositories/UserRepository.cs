using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Repositories
{
    [Repository]
    public class UserRepository: Repository<User>
    {
        public UserRepository(ISession session): base(session)
        {
            this.session = session;
        }        
    }
}
