using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Image image;
    [HideInInspector]
    public Transform parentAfterDrag;
    private RectTransform rectTransform;
    private Vector2 initialOffsetToMouse;
    private Canvas canvas;

    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Adjusted to get the nearest Canvas in the parent hierarchy
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform); // Ensure it's set to the canvas transform for consistent scaling
        image.raycastTarget = false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out initialOffsetToMouse);
        initialOffsetToMouse = rectTransform.anchoredPosition - initialOffsetToMouse;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            rectTransform.anchoredPosition = localPointerPosition + initialOffsetToMouse;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.SetAsLastSibling(); // Optional: Ensures the item stays on top of other UI elements within the same parent
        image.raycastTarget = true;
    }
}