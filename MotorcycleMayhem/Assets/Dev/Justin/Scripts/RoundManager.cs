using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    // Event serializations
    [Serializable] public class RoundStartEvent : UnityEvent { }
    [SerializeField] private RoundStartEvent OnRoundStart = new RoundStartEvent();

    [Serializable] public class GameStartEvent : UnityEvent { }
    [SerializeField] private GameStartEvent OnGameStart = new GameStartEvent();

    [Serializable] public class RoundEndEvent : UnityEvent { }
    [SerializeField] private RoundEndEvent OnRoundEnd = new RoundEndEvent();

    [Serializable] public class GameEndEvent : UnityEvent { }
    [SerializeField] private GameEndEvent OnGameEnd = new GameEndEvent();

    // Variable serializations
    [Range(3, 15)] public int amountOfRounds;
    private MultiplayerJoin multiplayerScript;
    private List<GameObject> players = new List<GameObject>();
    private int CurrentRound = 0;

    public void Start()
    {
        multiplayerScript = FindAnyObjectByType<MultiplayerJoin>();
        if (multiplayerScript == null )
        {
            Debug.LogError("Scene has no MultiplayerJoin");
            Destroy(this); return;
        }
        StartGame();
    }

    public void StartRound()
    {
        CurrentRound++;
        OnRoundStart.Invoke();
    }

    public void StartGame()
    {
        OnGameStart.Invoke();
    }

    public void EndRound()
    {
        if (CurrentRound <= amountOfRounds)
        {
            OnRoundEnd.Invoke();
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        OnGameEnd.Invoke();
    }
}
