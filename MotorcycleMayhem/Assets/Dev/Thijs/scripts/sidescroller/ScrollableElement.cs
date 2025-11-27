using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class ScrollableElement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 trueSpeed;
    private float scrollSpeed;
    private bool isBackground;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = Mathf.Pow(10, 9);
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        rb.useGravity = false;
    }

    public void Initialize(float speed, bool background)
    {
        trueSpeed = new Vector3(-speed, 0, 0);
        isBackground = background;
    }

    void Update()
    {
        rb.linearVelocity = trueSpeed;
    }

    public bool IsBackground()
    {
        return isBackground;
    }

    public void SetScrollSpeed(float newSpeed)
    {
        trueSpeed = new Vector3(-newSpeed, 0, 0);
    }

    public float GetScrollSpeed()
    {
        return trueSpeed.x;
    }


}