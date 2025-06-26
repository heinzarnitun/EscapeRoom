using UnityEngine;

public class SnapToCenter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))  // Or whatever your object's tag is
        {
            // Disable physics
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            // Move object to center of quad
            other.transform.position = transform.position + new Vector3(0, 0.5f, 0);  // Adjust Y offset if needed

            // Optional: parent it to quad if you want it to stay stuck
            other.transform.SetParent(transform);
        }
    }
}
