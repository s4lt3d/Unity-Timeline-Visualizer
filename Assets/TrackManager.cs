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

    public float TrackWidth = 1000;
    
    private void Start()
    {
        AddTrack();
        AddTrack();
        AddTrack();
        AddTrack();
        AddTrack();
        AddTrack();
        AddTrack();
    }

    public void AddTrack()
    {
        var newClipTrack = Instantiate(TrackPrefab, TrackPanel.transform);
        var newControlTrack = Instantiate(TrackControlPrefab, TrackControlPanel.transform);

        var clipTrackRectTransform = newClipTrack.GetComponent<RectTransform>();
        var controlTrackRectTransform = newControlTrack.GetComponent<RectTransform>();

        clipTrackRectTransform.sizeDelta = new Vector2(TrackWidth, 10);
        controlTrackRectTransform.sizeDelta = new Vector2(200, 10);

        var newClip = Instantiate(ClipPrefab, newClipTrack.transform);
        var newControl = Instantiate(ControlPrefab, newControlTrack.transform);

        var clipRectTransform = newClip.GetComponent<RectTransform>();
        var controlRectTransform = newControl.GetComponent<RectTransform>();

        clipRectTransform.sizeDelta = new Vector2(TrackWidth, 10);
        controlRectTransform.sizeDelta = new Vector2(100, 10);

        clipRectTransform.anchoredPosition = new Vector2(5, 0);
        controlRectTransform.anchoredPosition = new Vector2(5, 0);

        var newPivot = new Vector2(0, 0);

        clipRectTransform.pivot = newPivot;
        controlRectTransform.pivot = newPivot;

        LayoutRebuilder.ForceRebuildLayoutImmediate(TrackPanel.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(TrackControlPanel.GetComponent<RectTransform>());
    }

}