using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class VerticalMargins : MonoBehaviour
{
    [SerializeField] private float topMargin = 10f;
    [SerializeField] private float bottomMargin = 10f;

    private void Update()
    {

            AdjustChildLayouts();

    }

    private void AdjustChildLayouts()
    {
        if (!this.TryGetComponent<RectTransform>(out RectTransform rectTransform))
        {
            return;
        }

        float parentHeight = rectTransform.rect.height;
        float newChildHeight = parentHeight - (topMargin + bottomMargin);

        foreach (RectTransform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                // Ensure the pivot is set correctly for layout purposes
                SetPivot(child, new Vector2(child.pivot.x, 1));

                // Adjust child height and position
                child.sizeDelta = new Vector2(child.sizeDelta.x, newChildHeight);
                float startYPosition = -topMargin  * child.pivot.y; // Adjusts position based on pivot
                child.anchoredPosition = new Vector2(child.anchoredPosition.x, startYPosition);
            }
        }
    }

    #if UNITY_EDITOR
    private void SetPivot(RectTransform rectTransform, Vector2 newPivot)
    {
        if (rectTransform.pivot != newPivot)
        {
            //Undo.RecordObject(rectTransform, "Pivot Change"); // Record the action for undo functionality
            rectTransform.pivot = newPivot; // Set the new pivot

            // Adjust for the pivot change
            // This is a simplified example; you might need to adjust this based on your specific needs
            Vector2 deltaPosition = rectTransform.rect.size * (newPivot - rectTransform.pivot);
            Vector3 deltaPosition3D = new Vector3(deltaPosition.x, deltaPosition.y, 0);
            rectTransform.position += deltaPosition3D;
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
        }
    }
    #endif
}
