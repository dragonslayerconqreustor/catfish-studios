using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private InputAction actionAccelleration;
    private InputAction actionHandbrake;
    [SerializeField] private Rigidbody[] WheelsRb;
    [SerializeField, FormerlySerializedAs("speed")] private float fastAcceleration;
    [SerializeField] private float slowAcceleration;
    [SerializeField] private float maxSpeedForFastAcceleration;
    [SerializeField] private float readDelay;
    private WaitForSecondsRealtime readDelayTime;
    private Vector3 accel = new Vector3(0, 0, 0);
    Coroutine accelleratorTracker;
    [SerializeField] private float brakingPower;
    private float regularDampening;
    private bool handbrakeInitiated = false;
    
    void Start()
    {
        actionAccelleration = input.actions.FindAction("Accelleration");
        actionHandbrake = input.actions.FindAction("Stop");
        readDelayTime = new WaitForSecondsRealtime(readDelay);
        accelleratorTracker = StartCoroutine(Accelleration());
        regularDampening = WheelsRb[0].angularDamping;
    }

    void FixedUpdate()
    {
        /*
        Vector3 temp = new Vector3(0, 0, -speed * actionAccelleration.ReadValue<float>());
        */
        
        if (handbrakeInitiated)
        {
            foreach (Rigidbody rb in WheelsRb)
            {
                rb.angularVelocity = new Vector3(0, 0, 0);
            }
            accel = new Vector3(0, 0, 0);
        }
        else
        {
            foreach (Rigidbody rb in WheelsRb)
            {
                if (Mathf.Abs((WheelsRb[0].angularVelocity.z + WheelsRb[1].angularVelocity.z) / 2) < maxSpeedForFastAcceleration ||
                    actionAccelleration.ReadValue<float>() > 0 && (WheelsRb[0].angularVelocity.z + WheelsRb[1].angularVelocity.z) / 2 > 0 ||
                    actionAccelleration.ReadValue<float>() < 0 && (WheelsRb[0].angularVelocity.z + WheelsRb[1].angularVelocity.z) / 2 < 0)
                {
                    rb.AddTorque(accel * fastAcceleration, ForceMode.Acceleration);
                }
                else
                {
                    rb.AddTorque(accel * slowAcceleration, ForceMode.Acceleration);
                }
            }
        }
    }

    private IEnumerator Accelleration()
    {
        while (true)
        {
            handbrakeInitiated = false;
            accel = new Vector3(0, 0, -1 * actionAccelleration.ReadValue<float>());
            if (actionHandbrake.ReadValue<float>() > 0.5)
            {
                handbrakeInitiated = true;
            }
            yield return readDelayTime;
        }
    }
}
