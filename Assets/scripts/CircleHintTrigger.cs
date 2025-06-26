using UnityEngine;
using TMPro;

public class CircleHintTrigger : MonoBehaviour
{
    public TextMeshPro textObject;
    public AudioSource audioSource;  // Drag your AudioSource here in Inspector
    private string originalText;
    private bool isPlayerInside = false;

    void Start()
    {
        originalText = textObject.text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            textObject.text = "I am meant to \nbe thrown into \nthe waiting void.";
            isPlayerInside = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger");
            textObject.text = originalText;
            isPlayerInside = false;
        }
    }
}
