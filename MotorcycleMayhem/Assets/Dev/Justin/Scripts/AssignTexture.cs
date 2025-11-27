using UnityEngine;
using UnityEngine.InputSystem;

public class AssignTexture : MonoBehaviour
{
    [SerializeField] Material[] materials;
    void Start()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<Renderer>().material = materials[GetComponent<PlayerInput>().playerIndex];
        }
    }
}
