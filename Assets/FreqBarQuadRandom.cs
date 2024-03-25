using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FreqBarQuadRandom : MonoBehaviour
{
    private Material mat;
    private MeshRenderer mesh;
    private Material matClone;

    private float height = 0.5f;
    private float peak = 1f;
    private float newheight = 0.5f;
    private float newpeak = 1f;
    
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
        matClone = Instantiate(mat); // Clone the material

        height = Random.Range(0.0f, 0.9f);
        peak = Random.Range(0, 0.5f) + height;
        peak = Mathf.Min(1, peak);
        
        matClone.SetFloat("_Height", height);
        matClone.SetFloat("_Peak", peak);
        
        mesh.material = matClone;

        StartCoroutine(UpdateRandomly());
    }

    void Update()
    {
        newheight = Mathf.Lerp(newheight, height, Time.deltaTime * 10);
        newpeak = Mathf.Lerp(newpeak, peak, Time.deltaTime);
        newpeak = Mathf.Max(newheight, newpeak);
        matClone.SetFloat("_Height", newheight);
        matClone.SetFloat("_Peak", newpeak);
    }

    public IEnumerator UpdateRandomly()
    {
        while (true)
        {
            height = Random.Range(0.0f, 0.9f);
            peak = height;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
