using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BasketTriggerRoom2 : MonoBehaviour
{
    public string acceptedTag;
    public AudioSource successAudioSource;
    public AudioSource failAudioSource;
    public ParticleSystem basketParticle;
    public TextMeshProUGUI notificationText;
    public CountdownTimer countdownTimer;

    // Optional: assign this in inspector for game over trigger
    public GameOverHandlerRoom2 gameOverHandler;

    private Dictionary<int, float> recentObjects = new Dictionary<int, float>();
    public float objectCooldown = 3f;

    private bool gameOverTriggered = false;

    private void Start()
    {
        if (basketParticle != null)
        {
            var main = basketParticle.main;
            main.startColor = Color.white;
        }
    }

    private void Update()
    {
        // Check if time runs out and trigger game over once
        if (countdownTimer != null && !gameOverTriggered && countdownTimer.timeRemaining <= 0)
        {
            gameOverTriggered = true;
            Debug.Log("Time's up! Triggering game over.");

            if (gameOverHandler != null)
            {
                gameOverHandler.TriggerGameOver();
            }
            else
            {
                // Fallback: try to find GameOverHandlerRoom2 in scene if not assigned
                var handler = FindObjectOfType<GameOverHandlerRoom2>();
                if (handler != null)
                    handler.TriggerGameOver();
                else
                    Debug.LogWarning("No GameOverHandlerRoom2 found in scene!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int id = other.gameObject.GetInstanceID();

        if (recentObjects.ContainsKey(id) && Time.time - recentObjects[id] < objectCooldown)
        {
            Debug.Log("Skipping duplicate trigger for: " + other.gameObject.name);
            return;
        }

        recentObjects[id] = Time.time;

        Debug.Log("Something entered: " + other.gameObject.name);

        if (other.CompareTag(acceptedTag))
        {
            Debug.Log($"{acceptedTag} placed correctly!");

            if (successAudioSource != null)
                successAudioSource.Play();

            SetParticleColor(Color.green);
            ShowNotification($"{acceptedTag} is correct!");

            Room2GameManager.Instance.RegisterCorrect(other.gameObject);
        }
        else
        {
            Debug.Log("Wrong object placed!");

            if (failAudioSource != null)
                failAudioSource.Play();

            SetParticleColor(Color.red);
            ShowNotification("Wrong fruit! -1 min penalty");

            if (countdownTimer != null)
                countdownTimer.DeductTime(60f);
        }
    }

    void SetParticleColor(Color color)
    {
        if (basketParticle != null)
        {
            var main = basketParticle.main;
            main.startColor = color;
            basketParticle.Play();
        }
    }

    void ShowNotification(string message)
    {
        if (notificationText == null) return;

        StopAllCoroutines();
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);
        StartCoroutine(HideNotificationAfterDelay());
    }

    IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        notificationText.gameObject.SetActive(false);
        notificationText.text = "";
    }
}
