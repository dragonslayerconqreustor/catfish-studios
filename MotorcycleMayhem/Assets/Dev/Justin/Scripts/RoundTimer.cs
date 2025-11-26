using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RoundTimer : MonoBehaviour
{
    
    [SerializeField, Tooltip("This doesn't actually do anything in the code it's for usability")] private string label;
    [Serializable] public class TimerEndsEvent : UnityEvent { }
    [SerializeField] private TimerEndsEvent onTimerEnd = new TimerEndsEvent();

    [SerializeField, Min(0), Tooltip("The time the timer counts down from in seconds")] private float timerLength;
    private float timer;
    public int displayTime;
    private bool timerGoing = false;
    private bool timerPaused = false;
    private bool isTemporary = false;

    public RoundTimer(float timerLength)
    {
        this.timerLength = timerLength;
        isTemporary = true;
    }
    public void StartTimer()
    {
        if (!timerGoing)
        {
            timer = timerLength;
            timerGoing = true;
        }
    }

    public void PauseTimer()
    {
        timerPaused = true;
    }

    public void ContinueTimer()
    {
        timerPaused = false;
    }

    public void StopTimer()
    {
        timer = 0;
        timerGoing = false;
    }
    
    private void Update()
    {
        
        if (timerGoing && !timerPaused)
        {
            if (timer <= 0)
            {
                timerGoing = false;
                TimerEnd();
            }
            timer -= Time.deltaTime;
            displayTime = Mathf.RoundToInt(timer - 0.5f);
            if (displayTime < 0)
            {
                displayTime = 0;
            }
        }
    }

    private void TimerEnd()
    {
        onTimerEnd.Invoke();
        if (isTemporary)
        {
            Destroy(this);
        }
    }
}
