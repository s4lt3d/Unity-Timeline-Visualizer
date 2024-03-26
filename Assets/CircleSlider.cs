using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable] 
public class FillImageSettings
{
    public Image fillImage;
    public float minRotation = -130f;
    public float maxRotation = 130f;
    public float lerpRatio = 0.72f;
}

public class CircleSlider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private List<FillImageSettings> fillImagesSettings = new List<FillImageSettings>();

    private float currentValue = 1;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        // empty
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateUI(eventData);
        UpdateAudioSetting(currentValue);
    }

    private void UpdateUI(PointerEventData eventData)
    {
        Vector3 currentRotation = transform.eulerAngles;
        float newRotation = currentRotation.z - eventData.delta.x;
        float clampedZRotation = ClampAngle(newRotation, -130f, 130f);
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, clampedZRotation);

        foreach (var settings in fillImagesSettings)
        {
            settings.fillImage.fillAmount = settings.lerpRatio * Mathf.InverseLerp(settings.maxRotation, settings.minRotation, clampedZRotation);
        }
    }
    
    private void UpdateAudioSetting(float value)
    {
        // Update audio setting based on the currentValue
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // empty
    }

    float ClampAngle(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);
        min = NormalizeAngle(min);
        max = NormalizeAngle(max);
        return Mathf.Clamp(angle, min, max);
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180)
            angle -= 360;
        while (angle < -180)
            angle += 360;
        return angle;
    }
}

