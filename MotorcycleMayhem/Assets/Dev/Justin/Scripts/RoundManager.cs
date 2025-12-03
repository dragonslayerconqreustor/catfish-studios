using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : Util
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

    [Serializable] public class PointlessGameRoundEvent : UnityEvent { }
    [SerializeField] private PointlessGameRoundEvent OnPointlessRoundEnd = new PointlessGameRoundEvent();

    [Serializable] public class OnePlayerEvent : UnityEvent { }
    [SerializeField] private OnePlayerEvent OnOnePlayerLeft = new OnePlayerEvent();

    // Variable serializations
    [Range(3, 15)] public int amountOfRounds;
    private MultiplayerJoin multiplayerScript;
    private List<GameObject> players = new List<GameObject>();
    [HideInInspector] public List<bool> playersAlive = new List<bool>();
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
        foreach (PlayerInput player in multiplayerScript.activePlayers)
        {
            players.Add(player.gameObject);
        }
        for (int i = 0; i < players.Count; i++)
        {
            playersAlive.Add(true);
        }
    }

    public void StartRound()
    {
        CurrentRound++;
        for (int i = 0;i < playersAlive.Count; i++)
        {
            playersAlive[i] = true;
        }
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
        Debug.Log("Please");
        OnGameEnd.Invoke();
    }

    public void PointlessEndRound()
    {
        OnPointlessRoundEnd.Invoke();
    }

    private void WhenOnePlayerLeft()
    {
        OnOnePlayerLeft.Invoke();
    }

    private void Update()
    {
        if (CountAmountInList(playersAlive, true) == 1)
        {
            WhenOnePlayerLeft();
        }
        if (CountAmountInList(playersAlive, true) == 0)
        {
            Debug.Log("Why");
            PointlessEndRound();
        }
    }
}
