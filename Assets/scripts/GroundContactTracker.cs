using UnityEngine;

public class GroundContactTracker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Mark objects touching the ground
        collision.gameObject.tag = "OnGround";
    }
}
