using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverHandlerRoom2 : MonoBehaviour
{
    public GameObject waterLevelObject;         // assign your water object in inspector
    public TextMeshProUGUI gameOverText;        // assign UI text in inspector
    public AudioSource gameOverAudioSource;     // assign game over sound in inspector

    public float waterRiseSpeed = 1.5f;         // how fast water rises
    public float waterTargetHeight = 9f;        // final height

    private bool isGameOverTriggered = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // test shortcut key
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOverTriggered) return;

        isGameOverTriggered = true;

        // Play game over sound
        if (gameOverAudioSource != null)
            gameOverAudioSource.Play();

        // Start water rising
        if (waterLevelObject != null)
            StartCoroutine(RaiseWaterLevel());

        // Show game over text after delay and quit
        StartCoroutine(ShowGameOverMessageThenQuit());
    }

    private IEnumerator RaiseWaterLevel()
    {
        Vector3 targetPos = new Vector3(
            waterLevelObject.transform.position.x,
            waterTargetHeight,
            waterLevelObject.transform.position.z);

        while (waterLevelObject.transform.position.y < waterTargetHeight)
        {
            waterLevelObject.transform.position += Vector3.up * waterRiseSpeed * Time.deltaTime;
            yield return null;
        }

        waterLevelObject.transform.position = targetPos;
    }

    private IEnumerator ShowGameOverMessageThenQuit()
    {
        yield return new WaitForSeconds(5f);

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER!";
        }

        yield return new WaitForSeconds(3f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
