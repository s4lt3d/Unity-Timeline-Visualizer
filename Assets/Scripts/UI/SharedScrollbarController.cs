using UnityEngine;
using UnityEngine.UI;

public class SharedScrollbarController : MonoBehaviour
{
    public ScrollRect firstScrollView;
    public ScrollRect secondScrollView;
    public Scrollbar sharedScrollbar;

    private void Start()
    {
        sharedScrollbar.value = firstScrollView.verticalNormalizedPosition;
        sharedScrollbar.onValueChanged.AddListener(SetScrollViewsPosition);
    }

    private void SetScrollViewsPosition(float value)
    {
        firstScrollView.verticalNormalizedPosition = value;
        secondScrollView.verticalNormalizedPosition = value;
    }
}