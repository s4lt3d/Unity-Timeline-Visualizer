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
    TrackManager trackManager;
    
    private CancellationTokenSource cancellationTokenSource;
   
    async void Start()
    {
        serializer = ServiceLocator.GetService<IWaveformSerializer>();
        
        cancellationTokenSource = new CancellationTokenSource();
        
        waveformData = serializer.LoadFromFile(WaveformPath);
        serializer.SaveToFile(WaveformPath + "Copy", waveformData);

        foreach (var track in waveformData.Tracks)
        {
            List<(WaveformAudioClip, AudioClip)> clipsForTrack = new List<(WaveformAudioClip, AudioClip)>();

            foreach (var clip in track.AudioClips)
            {
                var audioClip = Instantiate(audioClipPrefab, transform);
                
                string audioName = clip.Name;
                
                string audioFilePath = $"file://D:\\UnityGames\\BeatConnectTechTest\\Assets\\Audio\\{audioName}.wav"; 

                AudioClip audioclipData = await AsyncAudioLoader.LoadAudioClipAsync(audioFilePath, cancellationTokenSource.Token);
                
                if (audioClip != null)
                {
                    clipsForTrack.Add((clip, audioclipData));
                }
            }

            if (clipsForTrack.Count > 0)
            {
                trackManager.AddTrack(clipsForTrack);
            }
        }
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
