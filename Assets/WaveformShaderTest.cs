using UnityEngine;

public class WaveformShaderTest : MonoBehaviour
{
    public Material waveformMaterial; // Assign in inspector if needed
    public Color lineColor = Color.red; 
    // Called before the first frame update
    void Start()
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

        // Update waveform for this GameObject
        UpdateWaveform(waveformTexture);
    }

    void UpdateWaveform(Texture2D waveformTexture)
    {
        var renderer = GetComponent<MeshRenderer>(); // Get the MeshRenderer component of this GameObject
        var propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);                  // Get the current property block
        propBlock.SetTexture("_WaveformTex", waveformTexture); // Set the waveform texture
        propBlock.SetColor("_LineColor", lineColor);           // Set the line color
        renderer.SetPropertyBlock(propBlock);                  // Apply the property block back to the renderer
    }
}