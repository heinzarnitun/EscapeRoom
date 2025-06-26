using UnityEditor;
using UnityEngine;

namespace FMS_SmartDoorToolkit
{
    [CustomEditor(typeof(KeyInventory))]
    public class KeyInventoryEditor : Editor
    {
        SerializedProperty interactionRange;//

        SerializedProperty keyLayer;//
        SerializedProperty doorLayer;//
        SerializedProperty inventory;//
        SerializedProperty keyUIPanel;//
        SerializedProperty keyUIPrefab;//
        SerializedProperty keyDetailsPanel;//
        SerializedProperty keyDetailsName;//

        SerializedProperty closeButton;//
        SerializedProperty dropButton;//
        SerializedProperty maxKeys;//
        SerializedProperty inventoryFullMessage;//
        SerializedProperty messageDuration;//


        private void OnEnable()
        {
            interactionRange = serializedObject.FindProperty("interactionRange");
            keyLayer = serializedObject.FindProperty("keyLayer");
            doorLayer = serializedObject.FindProperty("doorLayer");
            inventory = serializedObject.FindProperty("inventory");
            keyUIPanel = serializedObject.FindProperty("keyUIPanel");
            keyUIPrefab = serializedObject.FindProperty("keyUIPrefab");
            keyDetailsPanel = serializedObject.FindProperty("keyDetailsPanel");
            keyDetailsName = serializedObject.FindProperty("keyDetailsName");

            closeButton = serializedObject.FindProperty("closeButton");
            dropButton = serializedObject.FindProperty("dropButton");
            maxKeys = serializedObject.FindProperty("maxKeys");
            inventoryFullMessage = serializedObject.FindProperty("inventoryFullMessage");
            messageDuration = serializedObject.FindProperty("messageDuration");
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(keyLayer);
            EditorGUILayout.PropertyField(doorLayer);

            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(interactionRange);


            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(keyUIPanel);
            EditorGUILayout.PropertyField(keyUIPrefab);
            EditorGUILayout.PropertyField(inventory);




            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.PropertyField(keyDetailsPanel);
            EditorGUILayout.PropertyField(keyDetailsName);
            EditorGUILayout.PropertyField(closeButton);
            EditorGUILayout.PropertyField(dropButton);

            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(maxKeys);

            EditorGUILayout.PropertyField(inventoryFullMessage);
            EditorGUILayout.PropertyField(messageDuration);


            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();

        }

    }
}
