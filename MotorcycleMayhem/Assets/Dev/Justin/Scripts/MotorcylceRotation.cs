using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MotorcylceRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput input;
    [SerializeField] private float rotationMaxSpeed;
    [SerializeField] private float rotationAcceleration;
    [SerializeField] private float readDelay;
    private Vector3 rotationAccelVector;
    private WaitForSecondsRealtime readDelayVar;
    private InputAction actionRotation;

    private void Start()
    {
        actionRotation = input.actions.FindAction("Rotation");
        readDelayVar = new WaitForSecondsRealtime(readDelay);
        StartCoroutine(InputValueReader());
    }

    private void FixedUpdate()
    {
        if (!Physics.Raycast(transform.position, new Vector3(0, -1, 0), 0.75f))
        {
            rb.AddTorque(rotationAccelVector, ForceMode.Acceleration);
        }
    }

    private IEnumerator InputValueReader()
    {
        while (true)
        {
            float value = actionRotation.ReadValue<float>();
            rotationAccelVector = new Vector3(0, 0, -rotationAcceleration * value);
            if (value == 0)
            {
                rb.angularDamping = 10;
            }
            else
            {
                rb.angularDamping = 0.05f;
            }
            yield return readDelayVar;
        }
    }
}
