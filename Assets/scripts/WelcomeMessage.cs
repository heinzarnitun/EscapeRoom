using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeMessage : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;  // Assign your big welcome text UI here in Inspector
    public float displayDuration = 5f;   // How long the message stays visible

    void Start()
    {
        if (welcomeText != null)
        {
            Debug.Log("welcomeText GameObject active? " + welcomeText.gameObject.activeSelf);
            StartCoroutine(ShowWelcomeMessage());
        }
        else
        {
            Debug.LogWarning("WelcomeText is not assigned!");
        }
    }

    private IEnumerator ShowWelcomeMessage()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string message = "";

        switch (currentScene)
        {
            case "Room1":
                message = "Welcome to Room 1!\nFind clues to escape...";
                break;
            case "Room2":
                message = "Welcome to Room 2!\nIt gets harder — look carefully!";
                break;
            case "Room3":
                message = "Final Room!\nThis is your last chance to escape!";
                break;
            default:
                message = "Hello Player! Welcome to our escape room.";
                break;
        }

        welcomeText.gameObject.SetActive(true);
        welcomeText.text = message;

        yield return new WaitForSeconds(displayDuration);

        welcomeText.text = "";
        welcomeText.gameObject.SetActive(false);

        this.enabled = false; // disable this script after showing welcome
    }
}
