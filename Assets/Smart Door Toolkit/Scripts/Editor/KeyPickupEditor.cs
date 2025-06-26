using UnityEditor;
using UnityEngine;

namespace FMS_SmartDoorToolkit
{
    [CustomEditor(typeof(KeyPickup))]

    public class KeyPickupEditor : Editor
    {
        SerializedProperty keyData;//



        private void OnEnable()
        {
            keyData = serializedObject.FindProperty("keyData");
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(keyData);

            EditorGUILayout.EndVertical();



            serializedObject.ApplyModifiedProperties();

        }

    }
}
