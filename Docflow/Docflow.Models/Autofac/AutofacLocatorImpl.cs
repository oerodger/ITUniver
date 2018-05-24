using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Autofac
{
    public class AutofacLocatorImpl: ILocatorImpl
    {
        private IContainer container;

        public AutofacLocatorImpl(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type type)
        {
            object instance = null;
            container.TryResolve(type, out instance);
            return instance;
        }

        public void AddService(Type type, object obj)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveService(Type type)
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}
