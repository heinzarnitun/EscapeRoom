using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XRPlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // For example: move with keyboard arrows or WASD (replace with XR input later)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);
    }
}
