using UnityEngine;
using TMPro;

public class BasketTrigger : MonoBehaviour
{
    public AudioSource audioSource;        
    public TextMeshPro textObject;        
    public string message = @"In the month of hearts, 
luck turns thin,
The unluckiest number 
hides within.";


    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the ball
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball placed in basket!");

            if (audioSource != null)
                audioSource.Play();

            if (textObject != null)
                textObject.text = message;
        }
    }
}
