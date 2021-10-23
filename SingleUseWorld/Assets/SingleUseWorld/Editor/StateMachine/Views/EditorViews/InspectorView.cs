using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace SingleUseWorld.StateMachine.Views
{
    public class InspectorView : VisualElement
    {
        #region Nested Classes
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }
        #endregion

        #region Fields
        private Editor _inspectorEditor;
        #endregion

        #region Internal Methods
        internal void LoadSelection(ScriptableObject scriptableObject)
        {
            UnloadSelection();

            _inspectorEditor = Editor.CreateEditor(scriptableObject);
            IMGUIContainer inspectorContainer = new IMGUIContainer(() => 
            { 
                if(_inspectorEditor.target != null)
                    _inspectorEditor.OnInspectorGUI(); 
            });
            Add(inspectorContainer);
        }

        internal void UnloadSelection()
        {
            ClearPreviousSelection();
        }
        #endregion

        #region Private Methods
        private void ClearPreviousSelection()
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(_inspectorEditor);
        }
        #endregion
    }
}