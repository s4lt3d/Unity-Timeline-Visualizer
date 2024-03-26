using System;
using UnityEngine;

/// <summary>
/// This object should be used in any scene and should not kept between scenes
/// </summary>
public class ServiceInitializer : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Initialize(this.gameObject);
        RegisterServices();
        InitServices();
    }
    
    private void RegisterServices()
    {
        ServiceLocator.RegisterService<IWaveformSerializer>(new WaveformXMLSerializer());
        // place any other non-monobehaviour services here in the order they should be initialized
        
    }
    
    private void InitServices()
    {
        foreach(var service in ServiceLocator.GetAllServices())
        {
            service?.InitializeService();
        }
    }

    private void OnDestroy()
    {
        // Remove any services that are scene dependant. 
    }
}