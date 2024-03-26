using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CreateControlsInGrid : MonoBehaviour
{
    public Rect bounds;
    public int gridSize = 1;
    public List<GameObject> controls = new List<GameObject>();
    
    void Start()
    {
        Shuffle.Randomize(controls, controls[^1]);
        
        int index = 0;
        for (var i = bounds.x; i < bounds.width; i += gridSize)
        {
            for (var j = bounds.y; j < bounds.height; j += gridSize)
            {
                if (index >= controls.Count)
                {
                    Shuffle.Randomize(controls, controls[^1]);
                    index = 0;
                }

                var control = controls[index++];
                Instantiate(control, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }
}

public static class Shuffle
{
    public static void Randomize<T>(List<T> list, T prevEnd)
    {
        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        if (list.Count > 1 && EqualityComparer<T>.Default.Equals(list[0], prevEnd))
        {
            var j = Random.Range(1, list.Count);
            (list[0], list[j]) = (list[j], list[0]);
        }
    }
}