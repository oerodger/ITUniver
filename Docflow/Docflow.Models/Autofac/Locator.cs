using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docflow.Models.Autofac
{
    public class Locator
    {
        private static ILocatorImpl impl;

        public static object GetService(Type type)
        {
            return impl.GetService(type);
        }

        public static T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public static void AddService(Type type, object obj)
        {
            impl.AddService(type, obj);
        }

        public static void AddService<T>(T obj)
        {
            AddService(typeof(T), obj);
        }

        public static void RemoveService(Type type)
        {
            impl.RemoveService(type);
        }

        public static void Reset()
        {
            impl.Reset();
        }

        public static void SetImpl(ILocatorImpl locatorImpl)
        {
            impl = locatorImpl;
        }
    }
}
