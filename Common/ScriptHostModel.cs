using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Imaging;

namespace Common
{
    public class ScriptHostModel
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Register<T> (T t)
        {
            _services.Add(typeof(T), t);
        }

        public T GetService<T>()
        {
            return (T)_services[typeof (T)];
        }
    }
}