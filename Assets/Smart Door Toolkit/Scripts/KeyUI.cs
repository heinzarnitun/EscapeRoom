using UnityEngine;
using UnityEngine.UI;

namespace FMS_SmartDoorToolkit
{
    public class KeyUI : MonoBehaviour
    {
        [HideInInspector] public KeyItem keyData;
        public Image keyIcon;

        public void Initialize(KeyItem key)
        {
            keyData = key;
            keyIcon.sprite = key.keyIcon;

            // Add button listener for clicking the key in UI
            GetComponent<Button>().onClick.AddListener(ShowKeyDetails);
        }

        public void ShowKeyDetails()
        {

            KeyInventory.Instance.ShowKeyDetails(keyData);
            if (KeyInventory.Instance.keyInspection == null)
                KeyInventory.Instance.keyInspection = FindObjectOfType<KeyInspection>();
            KeyInventory.Instance.Inspect(keyData);
        }
    }
}
