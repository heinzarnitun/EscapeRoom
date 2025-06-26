using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FMS_SmartDoorToolkit
{
    public class KeyInventory : MonoBehaviour
    {
        public static KeyInventory Instance; // Singleton for easy access

        private HashSet<KeyItem> collectedKeys = new HashSet<KeyItem>();
        private KeyItem currentlySelectedKey; // Stores the currently viewed key

        public float interactionRange = 3f;
        public LayerMask keyLayer;
        public LayerMask doorLayer;
        public Transform inventory;
        private bool inventoryOpen = true;

        [Header("UI")]
        public Transform keyUIPanel; // Assign "KeyInventoryUI" Panel in Inspector
        public GameObject keyUIPrefab; // Assign "KeyUI_Prefab"

        [Header("Key Details UI")]
        public GameObject keyDetailsPanel;
        public TMP_Text keyDetailsName;
        public Button closeButton;
        public Button dropButton;

        [Header("Inventory Limit")]
        public int maxKeys = 8; // Maximum keys that can be stored
        public GameObject inventoryFullMessage; // Assign in Inspector (Message Panel)
        public float messageDuration = 2f; // How long the message stays visible

        [HideInInspector] public KeyInspection keyInspection;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            closeButton.onClick.AddListener(HideKeyDetails);
            dropButton.onClick.AddListener(DropKey);

            keyDetailsPanel.SetActive(false); // Start hidden
            inventoryFullMessage.SetActive(false); // Hide inventory full message initially
        }
        private void Start()
        {
            TryOpenInventory();


        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                TryPickupKey();
            if (Input.GetKeyDown(KeyCode.F))
                TryOpenDoor();
            if (Input.GetKeyDown(KeyCode.Tab))
                TryOpenInventory();
        }

        private void TryOpenInventory()
        {
            inventoryOpen = !inventoryOpen;

            if (inventoryOpen)
            {
                inventory.localPosition = Vector3.zero;
            }
            else
            {
                inventory.localPosition = new Vector3(10000, 0, 0);
            }
        }


        void TryOpenDoor()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, doorLayer))
            {
                DoorSystem door = hit.collider.GetComponent<DoorSystem>();
                if (door != null)
                {
                    door.Interact();
                }
            }
        }

        void TryPickupKey()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, keyLayer))
            {
                KeyPickup keyPickup = hit.collider.GetComponent<KeyPickup>();
                if (collectedKeys.Count >= maxKeys)
                {
                    ShowInventoryFullMessage();
                    return;
                }
                if (keyPickup != null)
                {
                    AddKey(keyPickup.keyData);
                    Debug.Log("Picked up and equipped key: " + keyPickup.keyData.keyName);
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        public void AddKey(KeyItem key)
        {


            if (!collectedKeys.Contains(key))
            {
                collectedKeys.Add(key);
                CreateKeyUI(key);
            }
        }
        public bool HasKey(KeyItem key)
        {
            return collectedKeys.Contains(key);
        }

        void CreateKeyUI(KeyItem key)
        {
            GameObject keyUIObj = Instantiate(keyUIPrefab, keyUIPanel);
            KeyUI keyUI = keyUIObj.GetComponent<KeyUI>();
            keyUI.Initialize(key);
        }

        public void ShowKeyDetails(KeyItem key)
        {


            currentlySelectedKey = key;
            keyDetailsPanel.SetActive(true);
            keyDetailsName.text = key.keyName;

        }

        public void HideKeyDetails()
        {
            keyDetailsPanel.SetActive(false);
            currentlySelectedKey = null;
        }

        public void DropKey()
        {
            if (currentlySelectedKey == null) return;

            // Remove from inventory
            collectedKeys.Remove(currentlySelectedKey);

            // Destroy UI element
            foreach (Transform child in keyUIPanel)
            {
                KeyUI keyUI = child.GetComponent<KeyUI>();
                if (keyUI != null && keyUI.keyData == currentlySelectedKey)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }

            // Instantiate key in the world at player's position
            if (currentlySelectedKey.keyPrefab != null)
            {
                GameObject droppedKey = Instantiate(
                    currentlySelectedKey.keyPrefab,
                    Camera.main.transform.position + Camera.main.transform.forward,
                    Quaternion.identity
                );

                KeyPickup keyPickup = droppedKey.GetComponent<KeyPickup>();
                if (keyPickup != null)
                {
                    keyPickup.keyData = currentlySelectedKey; // Assign correct key data
                }
            }
            else
            {
                Debug.LogWarning("No keyPrefab assigned for key: " + currentlySelectedKey.keyName);
            }

            // Hide details panel after dropping
            HideKeyDetails();
        }

        private void ShowInventoryFullMessage()
        {
            inventoryFullMessage.SetActive(true);
            CancelInvoke(nameof(HideInventoryFullMessage));
            Invoke(nameof(HideInventoryFullMessage), messageDuration);
        }

        private void HideInventoryFullMessage()
        {
            inventoryFullMessage.SetActive(false);
        }

        public void Inspect(KeyItem key)
        {
            keyInspection.SelectItem(key);
        }
    }
}
