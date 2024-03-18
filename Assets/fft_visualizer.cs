using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fft_visualizer : MonoBehaviour
{
    
    public AudioSource audioSource;
    public GameObject barPrefab;
    public int numberOfBars = 256;
    [SerializeField]
    private GameObject bar;
    private GameObject[] bars;

    void Start()
    {
        // Initialize bars based on the number of bars specified
        bars = new GameObject[numberOfBars];
        for (int i = 0; i < numberOfBars; i++)
        {
            GameObject barobj = Instantiate(bar, new Vector3(i * 0.05f, 0, 0), Quaternion.identity, transform);
            bars[i] = barobj;
        }
    }

    void Update()
    {
        float[] spectrum = new float[1024];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        int samplesPerBar = spectrum.Length / numberOfBars;

        for (int i = 0; i < numberOfBars; i++)
        {
            if (i < spectrum.Length)
            {
                // Scale the bars according to the spectrum data
                float height = Mathf.Clamp(spectrum[i] * (1 + i * i), 0.01f, 150f);
                bars[i].transform.localScale = new Vector3(0.04f, height, 00.04f);
            }
        }
    }
}

