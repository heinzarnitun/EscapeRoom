using System.Collections;
using TMPro;
using UnityEngine;

namespace FMS_SmartDoorToolkit
{
    public class PinPad : MonoBehaviour
    {
        public DoorSystem doorSystem;

        [Header("Audio & UI")]
        public AudioSource successAudioSource;   // Assign success sound in inspector
        public AudioSource failAudioSource;      // Assign fail sound in inspector
        public TextMeshPro pinDisplay;           // Small display for PIN digits
        public TMPro.TextMeshProUGUI notificationText;  // Big notification text for messages

        // NEW: Reference to CountdownTimer to listen for time expiry
        public CountdownTimer countdownTimer;

        // NEW: Reference to GameOverHandler to trigger game over when time expires
        public GameOverHandler gameOverHandler;

        void Start()
        {
            if (doorSystem == null)
                Debug.LogError($"[{gameObject.name}] PinPad's DoorSystem reference is STILL NULL at Start.");
            else
                Debug.Log($"[{gameObject.name}] PinPad's DoorSystem reference is assigned to hihi {doorSystem.gameObject.name}.");

            if (pinDisplay != null)
                pinDisplay.gameObject.SetActive(false);
            if (notificationText != null)
{
    notificationText.text = ""; // ← clear any leftover text content at start
    notificationText.gameObject.SetActive(false);
}


            // NEW: Subscribe to countdown timer event if assigned
            if (countdownTimer != null)
            {
                countdownTimer.OnTimeExpired += OnTimeExpired;
            }
            else
            {
                Debug.LogWarning("CountdownTimer reference is not assigned in PinPad!");
            }
        }

        // NEW: Unsubscribe on destroy to prevent memory leaks
        private void OnDestroy()
        {
            if (countdownTimer != null)
            {
                countdownTimer.OnTimeExpired -= OnTimeExpired;
            }
        }

        // NEW: This method is called when timer expires
        private void OnTimeExpired()
        {
            Debug.Log("Time expired! Triggering Game Over.");
            if (gameOverHandler != null)
            {
                gameOverHandler.TriggerGameOver();
            }
        }

        public void OnButtonPressed(string value)
        {

            if (doorSystem == null)
            {
                Debug.LogError("DoorSystem not assigned!");
                return;
            }

            doorSystem.EnterPinDigit(value);
            UpdatePinDisplay();
        }

        public void OnClearPressed()
        {
            doorSystem.ClearPin();
            if (pinDisplay != null)
            {
                pinDisplay.text = "PIN cleared";
                pinDisplay.gameObject.SetActive(true);
            }
        }

        public void OnEnterPressed()
        {
            if (doorSystem == null) return;

            if (doorSystem.CheckPinMatch())
            {
                successAudioSource?.Play();
                ShowNotification("You made it! You're on the next step.");
                pinDisplay.text = "Correct";
                pinDisplay.gameObject.SetActive(true);

                StartCoroutine(LoadSceneAfterWin());

            }
            else
            {
                failAudioSource?.Play();
ShowNotification("Incorrect PIN. One minute deducted.");

if (countdownTimer != null)
{
    countdownTimer.DeductTime(60f); // Deduct 60 seconds = 1 minute
}
else
{
    Debug.LogWarning("CountdownTimer reference not assigned in PinPad.");
}

pinDisplay.text = "Incorrect";
pinDisplay.gameObject.SetActive(true);

            }
        }

        public void OnLockPressed()
        {
            doorSystem.LockDoor();
            ShowNotification("Door Locked");
            UpdatePinDisplay();
        }

        private void UpdatePinDisplay()
        {
            if (pinDisplay == null) return;

            string currentPin = doorSystem.GetCurrentPin();
            pinDisplay.text = currentPin;
            pinDisplay.gameObject.SetActive(currentPin.Length > 0);
        }

        private void ShowNotification(string message)
{
    if (notificationText == null)
    {
        Debug.Log(message);
        return;
    }

    StopAllCoroutines();

    notificationText.text = ""; // ← force clear first  
    notificationText.gameObject.SetActive(false); // ← reset visibility

    notificationText.text = message;
    notificationText.gameObject.SetActive(true);

    StartCoroutine(HideNotificationAfterDelay());
}

private IEnumerator HideNotificationAfterDelay()
{
    yield return new WaitForSeconds(1f);
    notificationText.gameObject.SetActive(false);
    notificationText.text = "";
}

        private IEnumerator LoadSceneAfterWin()
{
    yield return new WaitForSeconds(5f);

    // Replace "NextSceneName" with your actual scene name
    UnityEngine.SceneManagement.SceneManager.LoadScene("Room2");
}

    }
}
