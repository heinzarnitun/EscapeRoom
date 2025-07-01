using UnityEngine;

public class FruitDetector : MonoBehaviour
{
    public float detectionRadius = 1f;
    public LayerMask fruitLayer; // Assign your fruit layer here in Inspector

    private void Update()
    {
        Collider[] detected = Physics.OverlapSphere(transform.position, detectionRadius, fruitLayer);

        foreach (Collider col in detected)
        {
            if (col.CompareTag("Fruit"))
            {
                Debug.Log("Fruit detected: " + col.name);
                col.GetComponent<FruitReveal>()?.Reveal();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize detection sphere in Editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
