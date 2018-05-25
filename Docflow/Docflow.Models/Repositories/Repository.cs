﻿using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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

        protected virtual void SetFetchOptions(ICriteria crit, FetchOptions options)
        {
            if (!string.IsNullOrEmpty(options.SortExpression))
            {
                crit.AddOrder(options.SortDirection == SortDirection.Ascending ?
                    Order.Asc(options.SortExpression) :
                    Order.Desc(options.SortExpression));
            }
        }

        public virtual IList<T> GetAll(FetchOptions options = null)
        {
            var crit = session.CreateCriteria<T>();
            if (options != null)
            {
                SetFetchOptions(crit, options);
            }
            return crit.List<T>();
        }

        public virtual void Save(T entity)
        {
            using (var tr = session.BeginTransaction())
            {
                session.Save(entity);
                tr.Commit();
            }
        }
    }
}
