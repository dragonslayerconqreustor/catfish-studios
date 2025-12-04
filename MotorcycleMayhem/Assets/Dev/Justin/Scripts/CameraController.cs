using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(BoxCollider))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private SplineContainer path;
    private CinemachineSplineDolly dolly;
    public float dollySpeed;
    
    private void Awake()
    {
        dolly = FindAnyObjectByType<CinemachineSplineDolly>();
        //UpdateDollySpeed(dollySpeed);
        UpdateDollySpeed(0f);
    }

    public void UpdateDollySpeed(float newSpeed, bool addToSpeed = false)
    {
        if (addToSpeed)
        {
            UpdateDollySpeed(dollySpeed + newSpeed);
            return;
        }
        var autoDolly = dolly.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
        if (autoDolly != null)
        {
            
            autoDolly.Speed = newSpeed / path.CalculateLength();
            dollySpeed = newSpeed;
        }
    }
    public void SetDollySpeed(float newSpeed)
    {
        var autoDolly = dolly.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
        if (autoDolly != null)
        {

            autoDolly.Speed = newSpeed / path.CalculateLength();
            dollySpeed = newSpeed;
        }
    }
    public void SetDollyPositionAlongTrack(float pos)
    {
        dolly.CameraPosition = pos;
    }
}
