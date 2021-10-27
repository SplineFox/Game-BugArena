using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using SingleUseWorld.StateMachine.Views;
using SingleUseWorld.StateMachine.Models;
using UnityEditor.Callbacks;
using System;

namespace SingleUseWorld.StateMachine.Windows
{
    public sealed class GraphEditorWindow : EditorWindow
    {
        #region Fields
        private GraphModel _graphAsset;

        private GraphView _graphView;
        private InspectorView _inspectorView;
        #endregion

        #region Private Methods
        private void OnDisable()
        {
            UnloadGraphAsset();
        }

        private void CreateGUI()
        {
            InitUxml();
            InitUss();
            InitElements();
            OnSelectionChange();
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

            _graphView.OnObjectSelected = OnObjectSelectionChange;
        }

        private void LoadGraphAsset(GraphModel graphAsset)
        {
            if (_graphAsset != graphAsset)
            {
                UnloadGraphAsset();

                _graphAsset = graphAsset;
                _graphView.LoadGraphModel(_graphAsset);
            }
        }
        private void UnloadGraphAsset()
        {
            _inspectorView.UnloadSelection();
            _graphView.UnloadGraphModel();
            _graphAsset = null;
        }

        private void UnloadGraphAssetIfDeleted()
        {
            if (_graphAsset == null)
            {
                UnloadGraphAsset();
            }
        }

        private void OnSelectionChange()
        {
            UnloadGraphAssetIfDeleted();

            GraphModel selectedAsset = Selection.activeObject as GraphModel;
            if (selectedAsset != null && AssetDatabase.GetAssetPath(selectedAsset) != "")
            {
                LoadGraphAsset(selectedAsset);
            }
        }

        private void OnObjectSelectionChange(ScriptableObject scriptableObject)
        {
            _inspectorView.LoadSelection(scriptableObject);
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