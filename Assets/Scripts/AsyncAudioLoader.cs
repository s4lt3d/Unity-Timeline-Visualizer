using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class AsyncAudioLoader
{
    public static async Task<AudioClip> LoadAudioClipAsync(string filePath, CancellationToken cancellationToken)
    {
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(filePath, DetermineAudioType(filePath)))
        {
            var operation = uwr.SendWebRequest();

            // Check for cancellation request while waiting for the request to complete
            while (!operation.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    uwr.Abort();
                    throw new TaskCanceledException("Audio loading was canceled.");
                }
                await Task.Delay(100, cancellationToken); // Wait a bit before checking again
            }

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerAudioClip.GetContent(uwr);
            }
            else
            {
                Debug.LogError($"Failed to load audio clip from path {filePath} with error: {uwr.error}");
                return null;
            }
        }
    }

    private static AudioType DetermineAudioType(string filePath)
    {
        // Simple example based on file extension, expand as needed
        if (filePath.EndsWith(".mp3")) return AudioType.MPEG;
        if (filePath.EndsWith(".wav")) return AudioType.WAV;
        if (filePath.EndsWith(".ogg")) return AudioType.OGGVORBIS;


        return AudioType.UNKNOWN;
    }
}