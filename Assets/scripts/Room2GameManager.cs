using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Room2GameManager : MonoBehaviour
{
    public static Room2GameManager Instance;

    public int correctCount = 0;
    public int totalFruitsNeeded = 3;
    private bool isLoadingNextRoom = false;

    // Track unique object IDs already counted
    private HashSet<int> countedObjects = new HashSet<int>();

    [Header("UI")]
    public TextMeshProUGUI notificationText;

    [Header("Audio")]
    public AudioSource successAudioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        // Test shortcut — press 'O' to trigger success manually
        if (Input.GetKeyDown(KeyCode.O) && !isLoadingNextRoom)
        {
            Debug.Log("Pressed O — forcing win");
            StartCoroutine(ShowWinAndLoadNext());
        }
    }

    public void RegisterCorrect(GameObject fruit)
    {
        int id = fruit.GetInstanceID();

        if (countedObjects.Contains(id))
        {
            Debug.Log($"[Room2GameManager] {fruit.name} already counted.");
            return;
        }

        countedObjects.Add(id);

        correctCount++;
        Debug.Log($"[Room2GameManager] Correct fruits: {correctCount} / {totalFruitsNeeded}");

        if (correctCount >= totalFruitsNeeded && !isLoadingNextRoom)
        {
            Debug.Log("[Room2GameManager] All correct — moving to Room 3...");
            StartCoroutine(ShowWinAndLoadNext());
        }
    }

    IEnumerator ShowWinAndLoadNext()
    {
        isLoadingNextRoom = true;

        notificationText.gameObject.SetActive(true);
        notificationText.text = "You made it! Moving to final room...";

        // Play success sound
        if (successAudioSource != null)
        {
            successAudioSource.Play();
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Room3");
    }
}
