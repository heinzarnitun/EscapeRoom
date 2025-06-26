using TMPro;
using UnityEngine;
using System;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 300; // 5 minutes
    public TextMeshPro countdownText;
    private bool timerIsRunning = true;

    public event Action OnTimeExpired;

    void Update()
    {
        if (timerIsRunning)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            if (OnTimeExpired != null)
                OnTimeExpired.Invoke();

            timeRemaining = 0;
            timerIsRunning = false;
            UpdateTimerDisplay();
        }
    }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void DeductTime(float seconds)
    {
        if (!timerIsRunning) return;

        timeRemaining -= seconds;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerIsRunning = false;
            OnTimeExpired?.Invoke();
        }
        UpdateTimerDisplay();
    }
}
