using UnityEngine;
using UnityEngine.UI;

public class SharedScrollbarController : MonoBehaviour
{
    public ScrollRect firstScrollView;
    public ScrollRect secondScrollView;
    public Scrollbar sharedScrollbar;

    private void Start()
    {
        // Optionally, initialize the scrollbar value or sync with one of the scroll views
        sharedScrollbar.value = firstScrollView.verticalNormalizedPosition;

        // Add listener to the shared scrollbar's value changed event
        sharedScrollbar.onValueChanged.AddListener(SetScrollViewsPosition);
    }

    private void SetScrollViewsPosition(float value)
    {
        // Apply the scrollbar value to both scroll views
        firstScrollView.verticalNormalizedPosition = value;
        secondScrollView.verticalNormalizedPosition = value;
    }
}