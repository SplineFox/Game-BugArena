using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

namespace SingleUseWorld.StateMachine.EditorTime
{
    internal class ScriptTemplates
    {
        #region Nested Classes
        private class DoCreateStateMachineScriptAsset : EndNameEditAction
        {
            #region Constants
            private const int EXTENSION_LENGTH = 3; // *[.cs]
            private const string SO_POSTFIX = "SO"; // *[SO].cs

            private const string SCRIPT_NAME_PLACEHOLDER = "#SCRIPT_NAME#";
            private const string RUNTIME_NAME_PLACEHOLDER = "#RUN_TIME_NAME#";
            private const string RUN_TIME_NAME_WITH_SPACES_PLACEHOLDER = "#RUN_TIME_NAME_WITH_SPACES#";
            #endregion

            #region Public Methods
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                // Example: "Action Name.cs"
                string fileName = Path.GetFileName(pathName);
                string scriptText = File.ReadAllText(resourceFile);

                // Remove spaces and add SO-postfix to file name if missing
                // Example: "ActionNameSO.cs"
                string newFileName = fileName.Replace(" ", "");
                if (!newFileName.Contains(SO_POSTFIX))
                    newFileName = newFileName.Insert(fileName.Length - EXTENSION_LENGTH, SO_POSTFIX);
                pathName = pathName.Replace(fileName, newFileName);
                fileName = newFileName;

                // Replace editor-time-name placeholder
                // Example: "ActionNameSO"
                string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - EXTENSION_LENGTH);
                scriptText = scriptText.Replace(SCRIPT_NAME_PLACEHOLDER, fileNameWithoutExtension);

                // Replace run-time-name placeholder
                // Example: "ActionName"
                string runTimeName = fileNameWithoutExtension.Replace(SO_POSTFIX, "");
                scriptText = scriptText.Replace(RUNTIME_NAME_PLACEHOLDER, runTimeName);

                // Replace asset-menu-name placeholder
                // Example: "Action Name"
                for (int index = runTimeName.Length - 1; index > 0; index--)
                    if (char.IsUpper(runTimeName[index]) && char.IsLower(runTimeName[index - 1]))
                        runTimeName = runTimeName.Insert(index, " ");
                scriptText = scriptText.Replace(RUN_TIME_NAME_WITH_SPACES_PLACEHOLDER, runTimeName);

                // Show asset
                string fullPath = Path.GetFullPath(pathName);
                var encoding = new UTF8Encoding(true);
                File.WriteAllText(fullPath, scriptText, encoding);
                AssetDatabase.ImportAsset(pathName);
                ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object)));
            }
            #endregion
        }
        #endregion

        #region Fields
        private static readonly string _path = "Assets/SingleUseWorld/Editor/StateMachine/ScriptTemplates";
        #endregion

        #region Static Methods
        [MenuItem("SingleUseWorld/StateMachine/Create Action Script", false, 0)]
        public static void CreateActionScript()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<DoCreateStateMachineScriptAsset>(),
                "NewActionSO.cs",
                (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
                $"{_path}/ActionTemplate.txt");
        }

        [MenuItem("SingleUseWorld/StateMachine/Create Statement Script", false, 0)]
        public static void CreateStatementScript()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<DoCreateStateMachineScriptAsset>(),
                "NewStatementSO.cs",
                (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
                $"{_path}/StatementTemplate.txt");
        }
        #endregion
    }
}