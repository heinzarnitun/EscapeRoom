using UnityEngine;

public class FruitReveal : MonoBehaviour
{
    public GameObject hiddenFruitObject;   // Assign the hidden fruit GameObject already placed in scene
    public AudioSource audioSource;        // Optional

    private bool isRevealed = false;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void Reveal()
    {
        if (isRevealed) return;
        isRevealed = true;

        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (hiddenFruitObject != null)
        {
            hiddenFruitObject.SetActive(true);
        }

        Debug.Log("Reveal called on: " + gameObject.name);
    }
}
