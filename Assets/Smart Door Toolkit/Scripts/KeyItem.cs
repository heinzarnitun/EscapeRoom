using UnityEngine;

namespace FMS_SmartDoorToolkit
{
    [CreateAssetMenu(fileName = "NewKey", menuName = "Smart Door Toolkit/Key Item")]
    public class KeyItem : ScriptableObject
    {
        public string keyName;
        public Sprite keyIcon;
        public GameObject keyPrefab;
    }
}
