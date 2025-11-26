using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private PositionDetection positionScript;
    [SerializeField] private MultiplayerJoin multiplayerScript;

    public int[] pointDistrubition =
    {
        -1,
        -1,
        -1,
        -1
    };

    public int[] positionValues =
    {
        50,
        35,
        25,
        15
    };

    public TextMeshPro[] leaderboardText;

    void Start()
    {
        if (positionScript == null)
        {
            positionScript = FindFirstObjectByType<PositionDetection>();
            if (positionScript == null)
            {
                Debug.LogError("Scene has no PositionDetection");
                Destroy(this); return;
            }
        }

        if (multiplayerScript == null)
        {
            multiplayerScript = FindAnyObjectByType<MultiplayerJoin>();
            if (multiplayerScript == null)
            {
                Debug.LogError("Scene has no MultiplayerJoin");
                Destroy(this); return;
            }
        }

        for (int i = 0; i < multiplayerScript.activePlayers.Count; i++)
        {
            pointDistrubition[i] = 0;
        }

        for (int i = pointDistrubition.Length - 1; pointDistrubition[i] == -1; i--)
        {
            leaderboardText[i].gameObject.SetActive(false);
        }
    }

    public void DistrubtePointsFromPosition()
    {
        for (int i = 0; i < multiplayerScript.activePlayers.Count; i++)
        {
            pointDistrubition[positionScript.positionedPlayerIndexes[i]] += positionValues[i];
        }
    }

    public void UpdateLeaderboard()
    {
        for (int i = 0; pointDistrubition[i] != -1; i++)
        {
            leaderboardText[i].text = ("player " + (i + 1) + ":" + pointDistrubition[i].ToString() + "Points");
        }
    }
}
