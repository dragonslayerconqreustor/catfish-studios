
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeathSettings : MonoBehaviour
{

    public bool isAlive = true;

    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private PositionDetection positionScript;
    private PlayerInput playerInput;
    private RoundManager roundManager;

    private void Start()
    {
        roundManager = FindAnyObjectByType<RoundManager>();
        if (roundManager == null )
        {
            Debug.LogError("Scene has no RoundManager");
            Destroy(this); return;
        }
    }

    public void Reload()
    {
        positionScript = FindAnyObjectByType<PositionDetection>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnDeath()
    {
        isAlive = false;
        positionScript.DecidePlacement(playerInput);
        roundManager.playersAlive[playerInput.playerIndex] = false;
        Debug.Log($"{gameObject.name} died. Settings saved.");

    }


    public void SaveCurrentSettings()
    {
        savedPosition = transform.position;
        savedRotation = transform.rotation;


        Debug.Log($"Settings saved for {gameObject.name}");
    }


    public void RestoreSettings()
    {
        transform.position = savedPosition;
        transform.rotation = savedRotation;
        isAlive = true;


        Debug.Log($"Settings restored for {gameObject.name}");
    }
}