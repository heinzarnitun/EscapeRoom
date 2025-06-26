using System.Collections;
using TMPro;
using UnityEngine;

public class WelcomeMessage : MonoBehaviour
{
    public TMPro.TextMeshProUGUI welcomeText;  // Assign your big welcome text UI here in Inspector
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
    welcomeText.gameObject.SetActive(true);
    welcomeText.text = "Hello Player! Welcome to our escape room.\nFind clues to escape...";

    yield return new WaitForSeconds(displayDuration);

    welcomeText.text = "";
    welcomeText.gameObject.SetActive(false);

    this.enabled = false; // disable this script after showing welcome
}



}
