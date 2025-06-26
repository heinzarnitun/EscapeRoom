using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    public float rotateSpeed = 10f;

    void Update()
    {
        // Spins horizontally around the Z axis
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
