using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RoundTimer : Util
{
    [SerializeField, Tooltip("This doesn't actually do anything in the code it's for usability"), TextArea] private string label;
    [SerializeField] private TimerEndsEvent onTimerEnd = new TimerEndsEvent();

    [SerializeField, Min(0), Tooltip("The time the timer counts down from in seconds")] private float timerLength;
    private float timer;
    public float timerSpeed = 1f;
    public int displayTime;
    [SerializeField] TextMeshProUGUI displayText;
    
    private bool timerGoing = false;
    private bool timerPaused = false;
    private bool isTemporary = false;

    public RoundTimer(float timerLength, float timerSpeed, TimerEndsEvent onTimerEnd)
    {
        this.timerLength = timerLength;
        this.timerSpeed = timerSpeed;
        this.onTimerEnd = onTimerEnd;
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
            timer -= Time.deltaTime * timerSpeed;
            displayTime = Mathf.RoundToInt(timer - 0.5f);
            if (displayTime < 0)
            {
                displayTime = 0;
            }
            if (displayText != null)
            {
                displayText.text = Mathf.Round(displayTime / 60 - 0.5f).ToString() + ":" + displayTime % 60;
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
