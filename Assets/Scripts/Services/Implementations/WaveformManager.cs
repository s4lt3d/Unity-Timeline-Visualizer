using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveformManager : MonoBehaviour, IService
{
    public string WaveformPath;
    
    public GameObject audioClipPrefab;
    
    IWaveformSerializer serializer;
    
    WaveformDataSet waveformData;
    
    public List<GameObject> audioClipPrefabs;
    
    [SerializeField]
    UITracksBuilder uiTracksBuilder;
    
    private CancellationTokenSource cancellationTokenSource;
   
    async void Start()
    {
        serializer = ServiceLocator.GetService<IWaveformSerializer>();
        
        cancellationTokenSource = new CancellationTokenSource();
        
        waveformData = serializer.LoadFromFile(WaveformPath);

        foreach (var track in waveformData.Tracks)
        {
            List<(WaveformAudioClip, AudioClip)> clipsForTrack = new List<(WaveformAudioClip, AudioClip)>();

            foreach (var clip in track.AudioClips)
            {
                var audioClip = Instantiate(audioClipPrefab, transform);
                
                string audioName = clip.Name;
                
                var audioFilePath = GetAudioPath(audioName);
 
                AudioClip audioclipData = await AsyncAudioLoader.LoadAudioClipAsync(audioFilePath, cancellationTokenSource.Token);
                
                if (audioClip != null)
                {
                    clipsForTrack.Add((clip, audioclipData));
                }
            }

            uiTracksBuilder.AddTrack(clipsForTrack);
        }
    }

    private static string GetAudioPath(string audioName)
    {
        // For future, use a service to get the audio path for a url or local asset bundle. 
        string audioFilePath = $"file://{Application.dataPath}/Audio/{audioName}.wav";
        return audioFilePath;
    }

    void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }

    public void InitializeService()
    {
        // empty
    }

    public void CleanupService()
    {
        // empty
    }
}
