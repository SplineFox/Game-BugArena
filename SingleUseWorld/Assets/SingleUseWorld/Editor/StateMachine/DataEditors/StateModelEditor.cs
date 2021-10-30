using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace SingleUseWorld.StateMachine.EditorTime
{
    [CustomEditor(typeof(StateModel))]
    public class StateModelEditor : Editor
    {
        #region Fields
        SerializedObject so;
        SerializedProperty propName;
        SerializedProperty propColor;
        SerializedProperty propActions;
        SerializedProperty propTransitions;

        ReorderableList reorderableTransitions;
        #endregion

        #region LifeCycle Methods
        private void OnEnable()
        {
            so = serializedObject;
            propName = so.FindProperty("_name");
            propColor = so.FindProperty("_color");
            propActions = so.FindProperty("_actions");
            propTransitions = so.FindProperty("_transitions");

            reorderableTransitions = new ReorderableList(so, propTransitions, true, true, false, false);
            reorderableTransitions.drawHeaderCallback += DrawTransitionListHeader;
            reorderableTransitions.drawElementCallback += DrawTransitionListElement;
        }
        #endregion

        #region Public Methods
        public override void OnInspectorGUI()
        {
            so.Update();
            EditorGUILayout.PropertyField(propName);
            EditorGUILayout.PropertyField(propColor);
            EditorGUILayout.PropertyField(propActions);
            reorderableTransitions.DoLayoutList();
            so.ApplyModifiedProperties();
        }
        #endregion

        #region Private Methods
        private void DrawTransitionListHeader(Rect rect)
        {
            string header = "Transitions";
            EditorGUI.LabelField(rect, header);
        }

        private void DrawTransitionListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propTransition = reorderableTransitions.serializedProperty.GetArrayElementAtIndex(index);
            SerializedObject objTransition = new SerializedObject(propTransition.objectReferenceValue);
            TransitionModel modelTransition = objTransition.targetObject as TransitionModel;

            string transitionName = modelTransition.DisplayName;

            Rect elementRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(rect, transitionName);
        }
        #endregion
    }
}