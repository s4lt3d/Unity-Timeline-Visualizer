using System.Collections.Generic;
using UnityEngine;
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

    // Adds all clips to a new track
    public void AddTrack(List<(WaveformDataSet, AudioClip)> clips)
    {
        var newClipTrack = Instantiate(TrackPrefab, TrackPanel.transform);
        var newControlTrack = Instantiate(TrackControlPrefab, TrackControlPanel.transform);

        var clipTrackRectTransform = newClipTrack.GetComponent<RectTransform>();
        var controlTrackRectTransform = newControlTrack.GetComponent<RectTransform>();

        clipTrackRectTransform.sizeDelta = new Vector2(TrackWidth, 10);
        controlTrackRectTransform.sizeDelta = new Vector2(200, 10);
        
        var newControl = Instantiate(ControlPrefab, controlTrackRectTransform);

        foreach (var (waveformClip, audioClip) in clips)
        {
            var clipLength = audioClip.length;
            var newClip = Instantiate(ClipPrefab, newClipTrack.transform);
            var clipRectTransform = newClip.GetComponent<RectTransform>();
            clipRectTransform.sizeDelta = new Vector2(clipLength * clipScale, 10);
            clipRectTransform.anchoredPosition = new Vector2(audioClip.Start * clipScale, 0);
            clipRectTransform.pivot = new Vector2(0, 0);

            var clipVisualizer = newClip.GetComponent<ClipAudioVisualizer>();
            clipVisualizer.audioClip = audioClip;
            clipVisualizer.Init();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(TrackPanel.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(TrackControlPanel.GetComponent<RectTransform>());
    }
}