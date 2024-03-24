using UnityEngine;

public class ServiceLocatorInitializer : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Initialize(this.gameObject);
        IntializeServices();
    }
    
    private void IntializeServices()
    {
        ServiceLocator.RegisterService<IWaveformSerializer>(new WaveformXMLSerializer());
        
        foreach(var service in ServiceLocator.GetAllServices())
        {
            if(service is IService serviceInstance)
            {
                serviceInstance.InitializeService();
            }
        }
    }
}