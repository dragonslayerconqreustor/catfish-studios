using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnPlayers : MonoBehaviour
{
    [SerializeField] private Transform[] respawnLocations;
    [SerializeField] private float respawnPointDistanceFromGround;
    [SerializeField] private RigidbodyConstraints contraints;
    public List<GameObject> players = new List<GameObject>();
    private List<Rigidbody> playerRbs = new List<Rigidbody>();
    [SerializeField] private MultiplayerJoin multiplayerScript;


    void Start()
    {
        if (multiplayerScript == null)
        {
            multiplayerScript = FindAnyObjectByType<MultiplayerJoin>();
            if (multiplayerScript == null)
            {
                Debug.LogError("Scene has no MultiplayerJoin");
                Destroy(this); return;
            }
        }

        foreach (PlayerInput player in multiplayerScript.activePlayers)
        {
            playerRbs.Add(player.gameObject.GetComponent<Rigidbody>());
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

        foreach (Transform point in respawnLocations)
        {
            RaycastHit hit;
            if (Physics.Raycast(point.transform.position, new Vector3(0, -1, 0), out hit, respawnPointDistanceFromGround))
            {
                point.transform.position += new Vector3(0, respawnPointDistanceFromGround - hit.distance, 0);
            }
        }
    }
}
