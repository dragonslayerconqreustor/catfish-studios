using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PositionDetection : Util
{
    // last place's index = amount of players - 1
    // Should keep ranking consistent regardless of number of players
    public int[] positionedPlayerIndexes =
    {
        -1,
        -1,
        -1,
        -1,
    };

    private MultiplayerJoin multiplayerScript;
    private List<int> activePLayerIndexes = new List<int>();

    void Start()
    {
        Application.targetFrameRate = 120;

        multiplayerScript = FindAnyObjectByType<MultiplayerJoin>();
        if (multiplayerScript == null )
        {
            Debug.LogError("Scene has no MultiplayerJoin");
            Destroy(this); return;
        }

        foreach (PlayerInput player in multiplayerScript.activePlayers)
        {
            activePLayerIndexes.Add(player.playerIndex);
        }
    }

    private void Update()
    {
        if (multiplayerScript.activePlayers.Count > activePLayerIndexes.Count)
        {
            activePLayerIndexes.Add(multiplayerScript.activePlayers[multiplayerScript.activePlayers.Count - 1].playerIndex);
        }
    }

    public void DecidePlacement(PlayerInput player)
    {
        for (int i = multiplayerScript.activePlayers.Count - 1; i >= 0; i--)
        {
            if (positionedPlayerIndexes[i] == -1)
            {
                positionedPlayerIndexes[i] = player.playerIndex;
                return;
            }
        }
    }

    public void DecidePlacement(int playerIndex)
    {
        for (int i = multiplayerScript.activePlayers.Count - 1; i >= 0; i--)
        {
            if (positionedPlayerIndexes[i] == -1)
            {
                positionedPlayerIndexes[i] = playerIndex;
                return;
            }
        }
    }

    public void ResetPlacements()
    {
        for (int i = 0; i < positionedPlayerIndexes.Length; i++)
        {
            positionedPlayerIndexes[i] = -1;
        }
    }

    public void DecidePlacementsFromPosition()
    {
        List<int> playersToTest;
        List<float> playersToTestXPositions = new List<float>();
        Dictionary<float, int> playerXToID = new Dictionary<float, int>();
        playersToTest = FindObjectsNotInList(activePLayerIndexes, positionedPlayerIndexes);

        foreach (int test in playersToTest)
        {
            playersToTestXPositions.Add(multiplayerScript.activePlayers[test].transform.position.x);
        }
        for (int i = 0;i < playersToTest.Count;i++)
        {
            playerXToID.Add(playersToTestXPositions[i], playersToTest[i]);
        }

        playersToTestXPositions = SortListOfNumbers(playersToTestXPositions);

        foreach (float xPos in playersToTestXPositions)
        {
            DecidePlacement(playerXToID[xPos]);
        }
    }
}
