using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using SingleUseWorld.StateMachine.Views;
using SingleUseWorld.StateMachine.Models;
using UnityEditor.Callbacks;

namespace SingleUseWorld.StateMachine.Windows
{
    public sealed class GraphEditorWindow : EditorWindow
    {
        #region Fields
        private GraphModel _graphModel;

        private GraphView _graphView;
        private InspectorView _inspectorView;
        #endregion

        #region Public Methods
        public void SetCurrentSelection(GraphModel graphModel)
        {
            if (_graphModel != graphModel)
            {
                _graphModel = graphModel;
                _graphView.SetModel(_graphModel);
            }
        }
        #endregion

        #region Private Methods
        private void CreateGUI()
        {
            InitUxml();
            InitUss();
            InitElements();
            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            GraphModel selectedAsset = Selection.activeObject as GraphModel;
            if (selectedAsset != null)
            {
                SetCurrentSelection(selectedAsset);
            }
        }

        private void InitUss()
        {
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/SingleUseWorld/Editor/StateMachine/Views/USS/StateGraphEditorWindow.uss");
            rootVisualElement.styleSheets.Add(uss);
        }

        private void InitUxml()
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/SingleUseWorld/Editor/StateMachine/Views/UXML/StateGraphEditorWindow.uxml");
            uxml.CloneTree(rootVisualElement);
        }

        private void InitElements()
        {
            _graphView = rootVisualElement.Q<GraphView>();
            _inspectorView = rootVisualElement.Q<InspectorView>();
        }
        #endregion


        #region Static Methods
        [MenuItem("SingleUseWorld/Window/StateGraphEditor")]
        public static void ShowEditorWindow()
        {
            FindOrCreateEditorWindow();
        }

        public static GraphEditorWindow FindOrCreateEditorWindow()
        {
            var window =  GetWindow<GraphEditorWindow>("StateGraphEditor");
            return window;
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if(Selection.activeObject is GraphModel)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }
        #endregion
    }
}