using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggbleItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [HideInInspector]
    public Image image;
    [HideInInspector]
    public Transform parentAfterDrag;
    private RectTransform rectTransform;
    private Vector2 initialOffset;
    
    private Vector2 initialOffsetToMouse;
    private Canvas canvas;
    
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        image.raycastTarget = false;
        canvas = transform.root.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        SetPointerOffsetOnBeginDrag(eventData);
    }

    private void SetPointerOffsetOnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null, out initialOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = eventData.position - initialOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
