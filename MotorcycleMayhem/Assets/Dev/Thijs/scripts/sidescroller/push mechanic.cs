using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpeedBoostTrigger : MonoBehaviour
{
    [Header("Speed Boost Settings")]
    [SerializeField] private float addSpeed = 1f;

    [SerializeField] private CameraController controller;

    private bool hasBoostedThisFrame = false;

    void Start()
    {
        if (controller == null)
        {
            controller = FindAnyObjectByType<CameraController>();
            if (controller == null)
            {
                Debug.LogError("Scene has no CameraController");
                Destroy(this); return;
            }
        }
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !hasBoostedThisFrame)
        {
            ActivateSpeedBoost();
        }
    }

    private void ActivateSpeedBoost()
    {
        controller.UpdateDollySpeed(addSpeed * Time.deltaTime, true);
        hasBoostedThisFrame = true;
        StartCoroutine(DontBoostMoreThanOncePerFrame());
    }

    private IEnumerator DontBoostMoreThanOncePerFrame()
    {
        yield return new WaitForEndOfFrame();
        hasBoostedThisFrame = false;
    }
}