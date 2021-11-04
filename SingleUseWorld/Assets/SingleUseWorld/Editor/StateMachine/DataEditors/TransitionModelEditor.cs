using UnityEditor;

namespace SingleUseWorld.StateMachine.Edittime
{
    [CustomEditor(typeof(TransitionModel))]
    public class TransitionModelEditor : Editor
    {
        #region Fields
        TransitionModel modelTransition;

        SerializedObject so;
        SerializedProperty propConditions;
        #endregion

        #region LifeCycle Methods
        private void OnEnable()
        {
            modelTransition = target as TransitionModel;

            so = serializedObject;
            propConditions = so.FindProperty("_conditions");
        }
        #endregion

        #region Public Methods
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField(modelTransition.DisplayName);

            so.Update();
            EditorGUILayout.PropertyField(propConditions);
            so.ApplyModifiedProperties();
        }
        #endregion
    }
}