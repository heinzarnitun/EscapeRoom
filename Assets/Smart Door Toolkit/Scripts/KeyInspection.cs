using FMS_SmartDoorToolkit;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FMS_SmartDoorToolkit
{
    public class KeyInspection : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private GameObject inspectionKey;

        public void SelectItem(KeyItem key)
        {
            if (inspectionKey != null)
            {
                Destroy(inspectionKey);
            }

            // Instantiate the key for inspection
            inspectionKey = Instantiate(key.keyPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);

            // Remove Rigidbody to prevent physics interactions
            Rigidbody rb = inspectionKey.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            PlayerMovement.IsInspecting = true; // Disable camera movement when dragging starts
        }

        public void OnDrag(PointerEventData eventData)
        {
            float rotationSpeed = 0.5f;

            Quaternion yRotation = Quaternion.Euler(0f, -eventData.delta.x * rotationSpeed, 0f);
            Quaternion xRotation = Quaternion.Euler(-eventData.delta.y * rotationSpeed, 0f, 0f);

            inspectionKey.transform.rotation = yRotation * inspectionKey.transform.rotation * xRotation;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            PlayerMovement.IsInspecting = false; // Enable camera movement when dragging stops
        }
    }
}
