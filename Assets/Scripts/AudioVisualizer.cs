using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVisualizer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private AudioClip audioClip;
    [SerializeField]
    private AudioSource audioSource;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        int width = 1920;
        int height = 20;

        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        Texture2D waveformTexture = new Texture2D(width, height);
        for (int i = 0; i < width; i++) {
            // Calculate the index in the samples array
            int sampleIndex = (int)((float)i / width * samples.Length);
            // Reset height for each pixel column based on amplitude
            int sampleHeight = Mathf.Clamp((int)(samples[sampleIndex] * height), 0, height);
            for (int j = 0; j < height; j++) {
                waveformTexture.SetPixel(i, j, j <= sampleHeight ? Color.white : Color.clear);
            }
        }
        waveformTexture.Apply();

        // Create a sprite from the texture
        Sprite waveformSprite = Sprite.Create(waveformTexture, new Rect(0.0f, 0.0f, width, height), new Vector2(0.5f, 0.5f), 100.0f);

        // Assign the sprite to the image
        image.sprite = waveformSprite;
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
