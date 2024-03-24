using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xmltest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WaveformXMLSerializer serializer = new WaveformXMLSerializer();
        var thing = serializer.LoadFromFile("D:\\UnityGames\\BeatConnect\\Interview.xml");
        serializer.SaveToFile("D:\\UnityGames\\BeatConnect\\Interview_test.xml", thing);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
