using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnPlayers : MonoBehaviour
{
    private Transform[] respawnLocations;
    [HideInInspector] public List<GameObject> players = new List<GameObject>();
    private List<Rigidbody> playerRbs = new List<Rigidbody>();
    private MultiplayerJoin multiplayerScript;


    void Start()
    {
        multiplayerScript = FindAnyObjectByType<MultiplayerJoin>();
        if (multiplayerScript == null)
        {
            Debug.LogError("Scene has no MultiplayerJoin");
            Destroy(this); return;
        }

        respawnLocations = FindFirstObjectByType<SpawnPoints>().spawnPoints;

        foreach (PlayerInput player in multiplayerScript.activePlayers)
        {
            playerRbs.Add(player.gameObject.GetComponent<Rigidbody>());
            players.Add(player.gameObject);
        }
    }

    public void StartRespawn()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(true);
            players[i].transform.position = respawnLocations[i].position;
            players[i].transform.rotation = respawnLocations[i].rotation;
            playerRbs[i].constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void FinishRespawn()
    {
        for (int i = 0;i < players.Count;i++)
        {
            playerRbs[i].constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        }
    }

    private void Update()
    {
        if (multiplayerScript.activePlayers.Count > playerRbs.Count)
        {
            playerRbs.Add(multiplayerScript.activePlayers[multiplayerScript.activePlayers.Count - 1].gameObject.GetComponent<Rigidbody>());
        }
    }
}
