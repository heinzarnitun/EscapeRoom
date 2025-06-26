using UnityEditor;
using UnityEngine;

namespace FMS_SmartDoorToolkit
{
    [CustomEditor(typeof(DoorSystem))]
    public class DoorSystemEditor : Editor
    {
        SerializedProperty doorType;//

        SerializedProperty anchorPosition;//
        SerializedProperty hingeAxis;//
        SerializedProperty openAngle;//
        SerializedProperty motorForce;//
        SerializedProperty motorSpeed;//
        SerializedProperty requiredKey;//
        SerializedProperty correctPin;//



        private void OnEnable()
        {
            doorType = serializedObject.FindProperty("doorType");
            anchorPosition = serializedObject.FindProperty("anchorPosition");
            hingeAxis = serializedObject.FindProperty("hingeAxis");
            openAngle = serializedObject.FindProperty("openAngle");
            motorForce = serializedObject.FindProperty("motorForce");
            motorSpeed = serializedObject.FindProperty("motorSpeed");
            requiredKey = serializedObject.FindProperty("requiredKey");
            correctPin = serializedObject.FindProperty("correctPin");
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(doorType);
            EditorGUILayout.EndVertical();


            if ((DoorSystem.DoorType)doorType.enumValueIndex == DoorSystem.DoorType.Basic)
            {
                EditorGUILayout.PropertyField(anchorPosition, true);
                EditorGUILayout.PropertyField(hingeAxis, true);
                EditorGUILayout.PropertyField(openAngle, true);
                EditorGUILayout.PropertyField(motorForce, true);
                EditorGUILayout.PropertyField(motorSpeed, true);


            }

            if ((DoorSystem.DoorType)doorType.enumValueIndex == DoorSystem.DoorType.KeyBased)
            {
                EditorGUILayout.PropertyField(anchorPosition, true);
                EditorGUILayout.PropertyField(hingeAxis, true);
                EditorGUILayout.PropertyField(openAngle, true);
                EditorGUILayout.PropertyField(motorForce, true);
                EditorGUILayout.PropertyField(motorSpeed, true);

                EditorGUILayout.PropertyField(requiredKey, true);

            }

            if ((DoorSystem.DoorType)doorType.enumValueIndex == DoorSystem.DoorType.PinBased)
            {
                EditorGUILayout.PropertyField(anchorPosition, true);
                EditorGUILayout.PropertyField(hingeAxis, true);
                EditorGUILayout.PropertyField(openAngle, true);
                EditorGUILayout.PropertyField(motorForce, true);
                EditorGUILayout.PropertyField(motorSpeed, true);

                EditorGUILayout.PropertyField(correctPin, true);

            }

            serializedObject.ApplyModifiedProperties();

        }

    }
}
