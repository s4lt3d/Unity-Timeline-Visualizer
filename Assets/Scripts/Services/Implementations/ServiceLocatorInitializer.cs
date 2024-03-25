using UnityEngine;

public class ServiceLocatorInitializer : MonoBehaviour
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
    }
    
    private void InitServices()
    {
        foreach(var service in ServiceLocator.GetAllServices())
        {
            service?.InitializeService();
        }
    }
}