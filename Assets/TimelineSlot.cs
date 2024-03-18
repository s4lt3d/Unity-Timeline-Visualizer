using UnityEngine;
using UnityEngine.EventSystems;

public class TimelineSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (!dropped.name.ToLower().Contains("clip"))
            return;
        if (dropped != null)
        {
            dropped.transform.SetParent(transform);
            DraggbleItem draggable = dropped.GetComponent<DraggbleItem>();
            if (draggable != null)
            {
                draggable.parentAfterDrag = transform;
            }

        }
    }
}
