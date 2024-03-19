using UnityEngine;
using UnityEngine.UI;

public class ClipAudioVisualizer : MonoBehaviour
{
    [SerializeField]
    private Image targetImage;
    public Material waveformMaterial; // Assign in inspector if needed
    public Color lineColor = Color.white; 
    void OnEnable()
    {

        int textureWidth = 4096; // Width of the texture
        Texture2D waveformTexture = new Texture2D(textureWidth, 1, TextureFormat.RFloat, false);
        
        // Generate random samples for demonstration
        float[] samples = new float[textureWidth];
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = Random.Range(-1f, 1f);
        }

        // Normalize and encode the samples into the texture
        for (int i = 0; i < textureWidth; i++)
        {
            float sample = (samples[i] + 1f) * 0.5f; // Normalize to 0..1
            waveformTexture.SetPixel(i, 0, new Color(sample, 0f, 0f, 1f));
        }
        waveformTexture.Apply();
        UpdateWaveform(waveformTexture, targetImage );
    }
    
    void UpdateWaveform(Texture2D waveformTexture, Image waveformImage)
    {
        
        Material newWaveformMaterial = Instantiate(waveformMaterial); // Clone the material
        
        // Convert the Texture2D to a Sprite to use in the UI Image
        Sprite waveformSprite = Sprite.Create(waveformTexture, new Rect(0.0f, 0.0f, waveformTexture.width, waveformTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        
        // Assign the texture to the cloned material
        newWaveformMaterial.SetTexture("_WaveformTex", waveformTexture);
        
        // Use the new material for this image
        waveformImage.material = newWaveformMaterial;

        // Optionally, set the color or other material properties as needed
        waveformImage.material.SetColor("_LineColor", lineColor);
        
        targetImage.sprite = waveformSprite;
    }
     
    void Update()
    {
        
    }
}
