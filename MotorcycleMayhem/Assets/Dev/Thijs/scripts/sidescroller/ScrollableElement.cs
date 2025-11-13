using UnityEngine;
using System.Collections.Generic;

public class ScrollableElement : MonoBehaviour
{
    private float scrollSpeed;
    private bool isBackground;

    public void Initialize(float speed, bool background)
    {
        scrollSpeed = speed;
        isBackground = background;
    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }

    public bool IsBackground()
    {
        return isBackground;
    }

    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }

    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }


}