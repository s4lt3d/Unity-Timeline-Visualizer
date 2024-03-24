using System;
using System.Collections.Generic;
using UnityEngine;

public static partial class ServiceLocator
{
    private static readonly IServiceContainer serviceContainer = new ServiceContainer();
    private static bool isInitialized = false;

    public static void Initialize(GameObject gameObject)
    {
        if (isInitialized == false)
        {
            isInitialized = true;
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }
    }

    public static void OnDestroy()
    {
        serviceContainer.Cleanup();
    }

    public static void RegisterService<T>(T serviceInstance) where T : IService
    {
        if (serviceContainer.HasService<T>())
            throw new ArgumentException(nameof(T), $"Service of type {nameof(T)} is already registered.");
        serviceContainer.AddService<T>(serviceInstance);
    }

    public static bool RemoveService<T>() where T : IService
    {
        return serviceContainer.RemoveService<T>();
    }

    public static bool HasService<T>() where T : IService
    {
        return serviceContainer.HasService<T>();
    }

    public static T GetService<T>() where T : IService
    {
        return (T)serviceContainer.GetService<T>();
    }
    
    public static IEnumerable<IService> GetAllServices()
    {
        return serviceContainer.GetAllServices();
    }
}