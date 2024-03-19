using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WaveformManager : MonoBehaviour
{
    public string WaveformPath;
    
    
    public GameObject audioClipPrefab;
    
    WaveformFileSerializer serializer;
    
    WaveformDataSet waveformData;
    
    public List<GameObject> audioClipPrefabs;
    
    [SerializeField]
    TrackManager trackManager;
    
    private CancellationTokenSource cancellationTokenSource;

    async void Start()
    {
        cancellationTokenSource = new CancellationTokenSource();
        serializer = new WaveformFileSerializer();
        waveformData = serializer.LoadFromFile(WaveformPath);
        
        

        foreach (var track in waveformData.Tracks)
        {
            


            foreach (var clip in track.AudioClips)
            {
                var audioClip = Instantiate(audioClipPrefab, transform);
                
                string audioName = clip.Name;
                
                string audioFilePath = $"file://D:\\UnityGames\\BeatConnectTechTest\\Assets\\Audio\\{audioName}.wav"; 
                
                
                try
                {
                    AudioClip audioclipData = await AsyncAudioLoader.LoadAudioClipAsync(audioFilePath, cancellationTokenSource.Token);

                    if (clip != null)
                    {
                        var audioSource = audioClip.GetComponent<AudioSource>();
                        
                        audioSource.clip = audioclipData;
    
                        audioClipPrefabs.Add(audioClip);
                        
                        trackManager.AddTrack(clip, audioSource.clip);
                        
                    }
                }
                catch (TaskCanceledException)
                {
                    Debug.Log("Audio loading was canceled.");
                }
                
               
            }
            
         


        }
        
    }
}
