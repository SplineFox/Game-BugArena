using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using SingleUseWorld.StateMachine.EditorTime;

namespace SingleUseWorld
{
    internal class GraphAssetCreationUtility
    {
        #region Nested Classes
        private class DoCreateStateMachineGraphAsset : EndNameEditAction
        {
            #region Fields
            GraphModel _graphAsset;
            #endregion

            #region Public Methods
            public void SetUp(GraphModel graphAsset)
            {
                _graphAsset = graphAsset;
            }

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                // We need to reset the selection, because it was set for a not yet fully created asset
                // when called StartNameEditingIfProjectWindowExists.
                Selection.activeObject = null;
                // After creating an asset in the database, graph asset has a path and it can be initialized
                // by adding sub-assets.
                AssetDatabase.CreateAsset(_graphAsset, AssetDatabase.GenerateUniqueAssetPath(pathName));
                // Initialize graph asset by creating and adding its default node to the asset.
                _graphAsset.CreateInitialNode();
                // We manualy set the selection to a new object after full initialization
                // so that this action can be processed by the graph editor window.
                Selection.activeObject = _graphAsset;
            }
            #endregion
        }
        #endregion

        [MenuItem("SingleUseWorld/StateMachine/Create Graph", false, 0)]
        public static void CreateGraphAsset()
        {
            var graphAsset = ScriptableObject.CreateInstance<GraphModel>();
            var endAction = ScriptableObject.CreateInstance<DoCreateStateMachineGraphAsset>();
            endAction.SetUp(graphAsset);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                graphAsset.GetInstanceID(),
                endAction,
                "NewGraphSO.asset",
                AssetPreview.GetMiniThumbnail(graphAsset),
                null);
        }
    }
}