using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public GameObject groundCube;                // assign ground cube in inspector
    public TextMeshProUGUI gameOverText;         // assign UI text in inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Example key to trigger
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
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

                // Very slow downward velocity and minimal side movement
                Vector3 randomVelocity = new Vector3(
                    Random.Range(-0.1f, 0.1f),
                    -1f,
                    Random.Range(-0.1f, 0.1f));
                rb.velocity = randomVelocity;

                // Very small torque for subtle spinning
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
        // Wait 2 seconds before showing the GAME OVER text
        yield return new WaitForSeconds(2f);

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER!";
        }

        // Wait 3 more seconds (total of 5s from trigger)
        yield return new WaitForSeconds(3f);

        // Stop play mode or quit app
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
