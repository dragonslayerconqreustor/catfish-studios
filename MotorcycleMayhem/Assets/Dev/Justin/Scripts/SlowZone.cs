using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent (typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]
public class SlowZone : MonoBehaviour
{
    [SerializeField] private bool isSlowZoneEntrance = true;
    [SerializeField, Tooltip("Only needed on slow zone exits, should be bound to that slow zone's entrance")] private SlowZone partnerEntrance;
    public float slowSpeed = 1f;
    [HideInInspector] public float storedCameraSpeed;

    private Rigidbody rb;
    private BoxCollider boxCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CameraController cam;
        if (other.gameObject.TryGetComponent<CameraController>(out cam))
        {
            if (isSlowZoneEntrance)
            {
                storedCameraSpeed = cam.dollySpeed - slowSpeed;
                cam.UpdateDollySpeed(slowSpeed);
            }
            else
            {
                cam.UpdateDollySpeed(partnerEntrance.storedCameraSpeed, true);
            }
        }
    }

}
