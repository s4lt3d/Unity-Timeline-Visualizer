using UnityEngine;

[ExecuteInEditMode]
public class VerticalMargins : MonoBehaviour
{
    [SerializeField]
    private float topMargin = 10f;

    [SerializeField]
    private float bottomMargin = 10f;

    private void Update()
    {
        AdjustChildLayouts();
    }

    private void AdjustChildLayouts()
    {
        if (!TryGetComponent(out RectTransform rectTransform)) return;

        var parentHeight = rectTransform.rect.height;
        var newChildHeight = parentHeight - (topMargin + bottomMargin);

        foreach (RectTransform child in transform)
            if (child.gameObject.activeInHierarchy)
            {
                SetPivot(child, new Vector2(child.pivot.x, 1));


                child.sizeDelta = new Vector2(child.sizeDelta.x, newChildHeight);
                var startYPosition = -topMargin * child.pivot.y;
                child.anchoredPosition = new Vector2(child.anchoredPosition.x, startYPosition);
            }
    }

#if UNITY_EDITOR
    private void SetPivot(RectTransform rectTransform, Vector2 newPivot)
    {
        if (rectTransform.pivot != newPivot)
        {
            rectTransform.pivot = newPivot;

            var deltaPosition = rectTransform.rect.size * (newPivot - rectTransform.pivot);
            var deltaPosition3D = new Vector3(deltaPosition.x, deltaPosition.y, 0);
            rectTransform.position += deltaPosition3D;
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
        }
    }
#endif
}