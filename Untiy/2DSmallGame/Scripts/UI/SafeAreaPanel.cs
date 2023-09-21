using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaPanel : MonoBehaviour
{
    private RectTransform panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<RectTransform>();

        Vector2 safeAreaMinPosition = Screen.safeArea.position;
        Vector2 safeAreaMaxPosition = Screen.safeArea.position + Screen.safeArea.size;

        safeAreaMinPosition.x = safeAreaMinPosition.x / Screen.width;
        safeAreaMinPosition.y = safeAreaMinPosition.y / Screen.height;

        safeAreaMaxPosition.x = safeAreaMaxPosition.x / Screen.width;
        safeAreaMaxPosition.y = safeAreaMaxPosition.y / Screen.height;

        panel.anchorMin = safeAreaMinPosition;
        panel.anchorMax = safeAreaMaxPosition;
    }
}
