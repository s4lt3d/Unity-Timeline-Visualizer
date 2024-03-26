using UnityEngine;

public class WaveyBob : MonoBehaviour
{
    float amplitude = 0.2f;
    float speed = 2.5f;
    float scale = 0.2f;

    private void Update()
    {
        var offset = scale * (transform.position.x + transform.position.z);
        transform.position = new Vector3(transform.position.x,
            Mathf.Sin((Time.realtimeSinceStartup + offset) * speed) *
            amplitude, transform.position.z);
    }
}