using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Callbacks;

namespace SingleUseWorld.StateMachine.EditorTime
{
    public sealed class GraphEditorWindow : EditorWindow
    {
        #region Fields
        private GraphModel _graphAsset;

        private GraphView _graphView;
        private InspectorView _inspectorView;
        #endregion

        #region Private Methods
        private void OnEnable()
        {
            InitializeWindow();
            ReloadGraphAsset();
        }

        private void OnDisable()
        {
            UnloadGraphAsset();
            DeinitializeWindow();
        }

        private void InitializeWindow()
        {
            InitializeUxml();
            InitializeUss();
            InitializeElements();
        }

        private void DeinitializeWindow()
        {
            rootVisualElement.Clear();
            _graphView = null;
            _inspectorView = null;
        }

        private void InitializeUss()
        {
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/SingleUseWorld/Editor/StateMachine/USS/StateGraphEditorWindow.uss");
            rootVisualElement.styleSheets.Add(uss);
        }

        private void InitializeUxml()
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/SingleUseWorld/Editor/StateMachine/UXML/StateGraphEditorWindow.uxml");
            uxml.CloneTree(rootVisualElement);
        }

        private void InitializeElements()
        {
            _graphView = rootVisualElement.Q<GraphView>();
            _inspectorView = rootVisualElement.Q<InspectorView>();

            _graphView.OnObjectSelected = OnObjectSelectionChange;
        }

        private void LoadGraphAsset(GraphModel graphAsset)
        {
            if(_graphAsset != graphAsset)
            {
                UnloadGraphAsset();
                _graphAsset = graphAsset;
                _graphView.LoadGraphModel(_graphAsset);
            }
        }

        private void ReloadGraphAsset()
        {
            // Try to restore an already opened graph after a reload of assemblies.
            if (_graphAsset != null)
            {
                _graphView.LoadGraphModel(_graphAsset);
                return;
            }
            // Try to get graph asset from currently selected item.
            OnSelectionChange();
        }

        private void UnloadGraphAsset()
        {
            _inspectorView.UnloadSelection();
            _graphView.UnloadGraphModel();
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
        [MenuItem("Window/State Machine/Graph Editor")]
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