using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public GameObject groundCube;                // assign ground cube in inspector
    public TextMeshProUGUI gameOverText;         // assign UI text in inspector
    public AudioSource gameOverAudioSource;      // assign game over sound in inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Example key to trigger
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        // Play game over sound
        if (gameOverAudioSource != null)
        {
            gameOverAudioSource.Play();
        }

        // Disable the ground
        if (groundCube != null)
        {
            groundCube.SetActive(false);
        }

        // Make everything fall
        MakeEverythingFall();

        // Start coroutine to show game over text after delay and quit
        StartCoroutine(ShowGameOverMessageThenQuit());
    }

    private void MakeEverythingFall()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (!obj.activeInHierarchy)
                continue;

            if (obj.CompareTag("NotFallable"))
                continue;

            if (obj.GetComponent<Canvas>() != null)
                continue;

            if (obj == gameOverText.gameObject || obj == groundCube)
                continue;

            if (obj.GetComponent<Rigidbody>() == null)
            {
                var controller = obj.GetComponent<CharacterController>();
                if (controller != null)
                    controller.enabled = false;

                var rb = obj.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;

                Vector3 randomVelocity = new Vector3(
                    Random.Range(-0.1f, 0.1f),
                    -1f,
                    Random.Range(-0.1f, 0.1f));
                rb.velocity = randomVelocity;

                Vector3 randomTorque = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f));
                rb.AddTorque(randomTorque, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator ShowGameOverMessageThenQuit()
    {
        yield return new WaitForSeconds(2f);

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
