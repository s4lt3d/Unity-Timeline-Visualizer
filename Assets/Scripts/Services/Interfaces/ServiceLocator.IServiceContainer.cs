using System.Collections.Generic;

public static partial class ServiceLocator
{
    private interface IServiceContainer
    {
        bool AddService<T>(object serviceInstance) where T : IService;
        bool RemoveService<T>(object serviceInstance) where T : IService;
        bool RemoveService<T>() where T : IService;
        bool HasService<T>() where T : IService;
        object GetService<T>() where T : IService;
        void Cleanup();
        IEnumerable<IService> GetAllServices();
    }
}