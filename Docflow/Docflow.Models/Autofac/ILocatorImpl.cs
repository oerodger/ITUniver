using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Autofac
{
    public interface ILocatorImpl
    {
        object GetService(Type type);

        void AddService(Type type, object obj);

        void RemoveService(Type type);

        void Reset();
    }
}
