using UnityEngine;
using UnityEditor;

namespace BugArena
{
    [CustomEditor(typeof(WorldGenerator))]
    public class WorldGeneratorEditor : Editor
    {
        #region LifeCycle Methods
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var worldGenerator = (WorldGenerator)target;
            if(GUILayout.Button("Generate world"))
            {
                worldGenerator.Generate();
            }
        }
        #endregion
    }
}