using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimelineSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            dropped.transform.SetParent(transform);
            DraggbleItem draggable = dropped.GetComponent<DraggbleItem>();
            if (draggable != null)
            {
                draggable.parentAfterDrag = transform;
            }
         //   dropped.transform.position = transform.position;
        }
    }
}
