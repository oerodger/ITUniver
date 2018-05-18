using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Repositories
{
    [Repository]
    public class UserRepository
    {
        private ISession session;

        public UserRepository(ISession session)
        {
            this.session = session;
        }

        public List<User> GetAll()
        {
            return session.CreateCriteria<User>()
                .List<User>()
                .ToList();
        }

        public void Save(User user)
        {
            using (var tr = session.BeginTransaction())
            {
                session.Save(user);
                tr.Commit();
            }
        }
    }
}
