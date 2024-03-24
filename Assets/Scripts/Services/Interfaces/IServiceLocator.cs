public interface IServiceLocator
{
    void AddService<T>(T serviceInstance) where T : IService;
    void RemoveService<T>(T serviceInstance) where T : IService;
    void RemoveService<T>() where T : IService;
    bool HasService<T>() where T : IService;
    T GetService<T>() where T : IService;
}