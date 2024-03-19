using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TrackManager : MonoBehaviour
{
    public GameObject TrackPanel;
    public GameObject TrackPrefab;

    public GameObject TrackControlPanel;
    public GameObject TrackControlPrefab;

    public GameObject ClipPrefab;
    public GameObject ControlPrefab;



    public float TrackWidth = 10000;


    public float clipScale = 100;

    

    // Called before the first frame update
    void Start()
    {

    }

    
    public void AddTrack(WaveformAudioClip clip, AudioClip audioClip)
    {
        var newClipTrack = Instantiate(TrackPrefab, TrackPanel.transform);
        var newControlTrack = Instantiate(TrackControlPrefab, TrackControlPanel.transform);

        var clipTrackRectTransform = newClipTrack.GetComponent<RectTransform>();
        var controlTrackRectTransform = newControlTrack.GetComponent<RectTransform>();

        clipTrackRectTransform.sizeDelta = new Vector2(TrackWidth, 10);
        controlTrackRectTransform.sizeDelta = new Vector2(200, 10);

        var newPivot = new Vector2(0, 0);
        
        var newControl = Instantiate(ControlPrefab, newControlTrack.transform);
        var controlRectTransform = newControl.GetComponent<RectTransform>();
        
        controlRectTransform.sizeDelta = new Vector2(100, 10);
        controlRectTransform.anchoredPosition = new Vector2(5, 0);
        controlRectTransform.pivot = newPivot;

        var clipLength = audioClip.length;
        
        var newClip = Instantiate(ClipPrefab, newClipTrack.transform);
        var clipRectTransform = newClip.GetComponent<RectTransform>();
        clipRectTransform.sizeDelta = new Vector2(clipLength * clipScale, 10);
        clipRectTransform.anchoredPosition = new Vector2(float.Parse(clip.Start) * clipScale, 0);
        clipRectTransform.pivot = newPivot;
        
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(TrackPanel.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(TrackControlPanel.GetComponent<RectTransform>());
        
        
    
    }
    



}