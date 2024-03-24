using System;
using UnityEngine;
using UnityEngine.Experimental.Audio;
using UnityEngine.UI;
using System.IO;

public class ClipAudioVisualizer : MonoBehaviour
{
    [SerializeField]
    private Image targetImage;
    public Material waveformMaterial; // Assign in inspector if needed
    public Color lineColor = Color.white;
    private Texture2D waveformTexture;
    int textureWidth = 4096; // Width of the texture

    public AudioClip audioClip;
    
    public void Init()
    {
        waveformTexture = new Texture2D(textureWidth, 1, TextureFormat.RFloat, false);

        
        float[] stereoSamples = new float[audioClip.samples * audioClip.channels];
        audioClip.GetData(stereoSamples, 0);
    
        // Extract just one channel (left)
        float[] monoSamples = new float[audioClip.samples];
        for (int i = 0, j = 0; i < stereoSamples.Length; i += 2, j++)
        {
            monoSamples[j] = stereoSamples[i];
        }
        
        var textureSamples = ResampleArray(monoSamples, textureWidth);

     //   DumpFloatArrayToFile(monoSamples, "D:\\UnityGames\\BeatConnectTechTest\\Assets\\Audio\\[90bpm] Cabasa(2)_raw.txt");
     //   DumpFloatArrayToFile(textureSamples, "D:\\UnityGames\\BeatConnectTechTest\\Assets\\Audio\\[90bpm] Cabasa(2)_resampled.txt");
        
        // Normalize and encode the samples into the texture
        for (int i = 0; i < textureWidth; i++)
        {
            float sample = textureSamples[i];
            float magnitude = Mathf.Abs(sample) * 0.5f; // Normalize magnitude to 0..0.5
            float sign = sample >= 0 ? 1.0f : 0.5f;     // Encode phase in the sign

            // Store magnitude in the red channel and phase in the green channel
            waveformTexture.SetPixel(i, 0, new Color(magnitude, sign, 0f, 1f));
        }
        
        waveformTexture.Apply();
      //  waveformTexture.Apply();
     //   waveformTexture.Apply();
    //    SaveTextureAsPNG(waveformTexture, "D:\\UnityGames\\BeatConnectTechTest\\Assets\\Audio\\waveform.png");
        
        UpdateWaveform(waveformTexture, targetImage );
    }
    
    void UpdateWaveform(Texture2D waveformTexture, Image waveformImage)
    {
        
        Material newWaveformMaterial = Instantiate(waveformMaterial); // Clone the material
         
        // Convert the Texture2D to a Sprite to use in the UI Image
        Sprite waveformSprite = Sprite.Create(waveformTexture, new Rect(0.0f, 0.0f, waveformTexture.width, waveformTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        
        // Assign the texture to the cloned material
        newWaveformMaterial.SetTexture("_WaveformTex", waveformTexture);
        newWaveformMaterial.SetColor("_LineColor", lineColor);
      //  newWaveformMaterial.SetFloat("_LineHeight", 0.75f);
        
        
        
        // Use the new material for this image
        waveformImage.material = newWaveformMaterial;

        
        targetImage.sprite = waveformSprite;
    }
     
    
    
    
    
    public static float[] ResampleArray(float[] original, int newSize)
    {
        if (newSize <= 0)
        {
            throw new System.ArgumentException("New size must be greater than 0.", nameof(newSize));
        }

        float[] resampled = new float[newSize];
        float scale = (float)(original.Length - 1) / (newSize - 1);

        for (int i = 0; i < newSize; i++)
        {
            if (i == newSize - 1)
            {
                // Handle the last index to avoid accessing out of bounds
                resampled[i] = original[original.Length - 1];
            }
            else
            {
                // Calculate the corresponding index in the original array
                float originalIndex = i * scale;
                int index = (int)originalIndex;
                float fraction = originalIndex - index;

                // Linear interpolation using Mathf.Lerp
                resampled[i] = Mathf.Lerp(original[index], original[index + 1], fraction);
            }
        }

        return resampled;
    }
    
    
    void Update()
    {
        
      
    }
    
    public static void DumpFloatArrayToFile(float[] array, string filePath)
    {
        // Use StreamWriter to open the file for writing
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Iterate through the array and write each element to the file
            foreach (float value in array)
            {
                writer.WriteLine(value);
            }
        }
        
        
        
        
    }
    
    public void SaveTextureAsPNG(Texture2D texture, string filePath)
    {
        byte[] bytes = texture.EncodeToPNG();
        if (bytes != null && bytes.Length > 0)
        {
            System.IO.File.WriteAllBytes(filePath, bytes);
            Debug.Log("Saved texture to " + filePath);
        }
    }

}
