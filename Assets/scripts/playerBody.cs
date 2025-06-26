using UnityEngine;

public class PlayerBodyFollow : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        Vector3 pos = cameraTransform.position;
        pos.y = transform.position.y; // Keep body Y position constant (or set as needed)
        transform.position = pos;
    }
}
