using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class MultiplayerJoin : MonoBehaviour
{
    [Header("Setup")]
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public int maxPlayers = 4;

    private PlayerInputManager inputManager;
    public List<PlayerInput> activePlayers = new List<PlayerInput>();

    void Awake()
    {
        DontDestroyOnLoad(this);
        inputManager = GetComponent<PlayerInputManager>();
        inputManager.playerPrefab = playerPrefab;
        inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;

        inputManager.playerJoinedEvent.AddListener(OnPlayerJoined);
        inputManager.playerLeftEvent.AddListener(OnPlayerLeft);
        Reload();
    }

    void OnDestroy()
    {
        inputManager.playerJoinedEvent.RemoveListener(OnPlayerJoined);
        inputManager.playerLeftEvent.RemoveListener(OnPlayerLeft);
    }


    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if (activePlayers.Count >= maxPlayers)
        {
            Destroy(playerInput.gameObject);
            return;
        }
        activePlayers.Add(playerInput);

        int playerIndex = activePlayers.Count - 1;
 
        if (spawnPoints.Length > 0)
        {
            Transform spawn = spawnPoints[playerIndex % spawnPoints.Length];
            playerInput.transform.position = spawn.position;
            playerInput.transform.rotation = spawn.rotation;
        }
        Debug.Log($"Player {playerIndex + 1} joined with device: {playerInput.devices[0].displayName}");
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        activePlayers.Remove(playerInput);
        Debug.Log("Player left. Remaining: " + activePlayers.Count);
    }

    public void Reload(bool enableJoining = false)
    {
        try
        {
            spawnPoints = FindFirstObjectByType<SpawnPoints>().spawnPoints;
            if (spawnPoints.Length == 0 || spawnPoints == null)
            {
                Debug.LogWarning("Scene has no Spawnpoints");
            }
        }
        catch (Exception e)
        { 
            Debug.LogWarning(e + "Scene has no Spawnpoints");
        }
        if (enableJoining)
            inputManager.EnableJoining();
        else
            inputManager.DisableJoining();
        foreach (var player in activePlayers)
        {
            player.gameObject.GetComponent<PlayerDeathSettings>().Reload();
        }
    }

    public void ResetSystem()
    {
        foreach (PlayerInput player in activePlayers)
        {
            Transform temp = player.transform;
            while (temp.parent != null)
            {
                temp = temp.parent;
            }
            Destroy(temp.gameObject);
        }
        activePlayers = new List<PlayerInput>();
    }
}
