using System;
using System.Collections.Generic;

namespace SingleUseWorld
{
    public class DIContainer
    {
        private readonly DIContainer _parent;
        private readonly Dictionary<Type, object> _instances;
        
        public DIContainer(DIContainer parent = null)
        {
            _parent = parent;
            _instances = new Dictionary<Type, object>();
        }

        public void Register<T>(T instance)
        {
            _instances.Add(typeof(T), instance);
        }

        public T Resolve<T>()
        {
            if (_instances.TryGetValue(typeof(T), out object instance))
                return (T)instance;

            if (_parent != null)
                return _parent.Resolve<T>();

            throw new InvalidOperationException($"Dependency \"{typeof(T)}\" not found");
        }
    }
}