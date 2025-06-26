using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FloatingBall : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 0.5f;

    private Vector3 startPos;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private bool isFloating = true;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void Update()
    {
        if (isFloating)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        isFloating = false;
        rb.isKinematic = false;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        // No floating again — leave physics on
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }
}
