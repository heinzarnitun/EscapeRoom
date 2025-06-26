using UnityEditor;
using UnityEngine;

namespace FMS_SmartDoorToolkit
{

    [CustomEditor(typeof(KeyUI))]

    public class KeyUIEditor : Editor
    {
        SerializedProperty keyIcon;//



        private void OnEnable()
        {
            keyIcon = serializedObject.FindProperty("keyIcon");
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(keyIcon);

            EditorGUILayout.EndVertical();



            serializedObject.ApplyModifiedProperties();

        }
    }
}