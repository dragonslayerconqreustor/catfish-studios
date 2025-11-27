using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    [Header("Speed Boost Settings")]
    [SerializeField] private float backgroundSpeedMultiplier = 2f;
    [SerializeField] private float foregroundSpeedMultiplier = 2f;

    [Header("References")]
    [SerializeField] private SideScrollingManager scrollingManager;

    private float originalBackgroundSpeed;
    private float originalForegroundSpeed;
    private bool isSpeedBoosted = false;

    void Start()
    {

        if (scrollingManager == null)
        {
            scrollingManager = FindFirstObjectByType<SideScrollingManager>();
        }

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }
        boxCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !isSpeedBoosted)
        {
            ActivateSpeedBoost();
        }
    }

    void OnTriggerExit(Collider other)
    {
     
        if (other.CompareTag("Player") && isSpeedBoosted)
        {
            DeactivateSpeedBoost();
        }
    }

    private void ActivateSpeedBoost()
    {
        if (scrollingManager == null) return;

        originalBackgroundSpeed = scrollingManager.backgroundScrollSpeed;
        originalForegroundSpeed = scrollingManager.foregroundScrollSpeed;

        scrollingManager.backgroundScrollSpeed *= backgroundSpeedMultiplier;
        scrollingManager.foregroundScrollSpeed *= foregroundSpeedMultiplier;

        isSpeedBoosted = true;
        Debug.Log("Speed Boost Activated!");
    }

    private void DeactivateSpeedBoost()
    {
        if (scrollingManager == null) return;


        scrollingManager.backgroundScrollSpeed = originalBackgroundSpeed;
        scrollingManager.foregroundScrollSpeed = originalForegroundSpeed;

        isSpeedBoosted = false;
        Debug.Log("Speed Boost Deactivated!");
    }
}