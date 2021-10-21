using UnityEditor;
using UnityEngine;

namespace SingleUseWorld
{
    [CustomEditor(typeof(TestSO))]
    public class TestSOEditor : Editor
    {
        SerializedObject so;
        SerializedProperty propFloat;
        SerializedProperty propColor;

        private void OnEnable()
        {
            so = serializedObject;
            propFloat = so.FindProperty("floatField");
            propColor = so.FindProperty("colorField");
        }

        public override void OnInspectorGUI()
        {
            TestSO testSO = target as TestSO;
            so.Update();
            EditorGUILayout.PropertyField(propFloat);
            EditorGUILayout.PropertyField(propColor);
            so.ApplyModifiedProperties();
        }
    }
}