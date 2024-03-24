using System;
using System.Collections.Generic;

public static partial class ServiceLocator
{
    private class ServiceContainer : IServiceContainer
    {
        private Dictionary<Type, object> container = new();

        bool IServiceContainer.AddService<T>(object serviceInstance)
        {
            var type = typeof(T);

            if (serviceInstance == null)
                throw new ArgumentNullException(nameof(serviceInstance), "Service instance cannot be null.");

            if (serviceInstance is not T)
                throw new ArgumentNullException(nameof(serviceInstance), $"Service instance must match type {type}.");

            if (!container.TryAdd(type, serviceInstance))
                throw new ArgumentNullException(nameof(serviceInstance), $"Type of {type} already registered.");

            return true;
        }

        bool IServiceContainer.RemoveService<T>(object serviceInstance)
        {
            if (container.ContainsKey(typeof(T)))
                return false;

            return RemoveService<T>();
        }

        bool IServiceContainer.RemoveService<T>()
        {
            container.Remove(typeof(T));
            return true;
        }

        bool IServiceContainer.HasService<T>()
        {
            return container.ContainsKey(typeof(T));
        }

        object IServiceContainer.GetService<T>()
        {
            if (container.TryGetValue(typeof(T), out var obj))
                return obj;

            throw new ArgumentNullException(nameof(T), $"Service of type {nameof(T)} is not registered.");
        }

        void IServiceContainer.Cleanup()
        {
            if (container == null)
                return;

            foreach (var service in container.Values)
                if (service != null)
                {
                    var s = service as IService;
                    if (s == null)
                        continue;
                    s.CleanupService();
                }
        }

        public IEnumerable<IService> GetAllServices()
        {
            List<IService> services = new();
            foreach(var service in container.Values)
            {
                if(service is IService serviceInstance)
                {
                    services.Add(serviceInstance);
                }
            }

            return services;
        }
    }
}