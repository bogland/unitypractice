using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public Vector2 MinAnchor;
    public Vector2 MaxAnchor;
    RectTransform _rectTransform;
    Rect _safeArea;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _safeArea = Screen.safeArea;
        MinAnchor = _safeArea.position;
        MaxAnchor = MinAnchor + _safeArea.size;

        MinAnchor.x /= Screen.width;
        MinAnchor.y /= Screen.height;
        MaxAnchor.x /= Screen.width;
        MaxAnchor.y /= Screen.height;

        _rectTransform.anchorMin = MinAnchor;
        _rectTransform.anchorMax = MaxAnchor;
    }
}