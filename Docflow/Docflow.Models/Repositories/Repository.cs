using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Repositories
{
    public abstract class Repository<T>
        where T:class
    {
        protected ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public virtual T Load(long id)
        {
            return session.Load<T>(id);
        }

        public virtual IList<T> GetAll()
        {
            return session.CreateCriteria<T>().List<T>();
        }

        public virtual void Save(T entity)
        {
            using (session.BeginTransaction())
            {
                session.Save(entity);
            }
        }
    }
}
