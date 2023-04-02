using System.Linq;
using UnityEngine;
using UnityEditor;

namespace BugArena
{
    [CustomPropertyDrawer(typeof(ColoredHeaderAttribute))]
    internal sealed class ColoredHeaderDrawer : DecoratorDrawer
    {
        private ColoredHeaderAttribute coloredHeaderAttribute
        {
            get => (ColoredHeaderAttribute)attribute;
        }

        public override void OnGUI(Rect position)
        {
            position = EditorGUI.IndentedRect(position);
            position.yMin += EditorGUIUtility.singleLineHeight * 0.5f;

            GUIStyle style = new GUIStyle(EditorStyles.boldLabel) { richText = true };
            var richTextMarkup =
                $"<color={coloredHeaderAttribute.colorHTML}>" +
                    $"<size={style.fontSize}>" +
                        $"<b>{coloredHeaderAttribute.header}</b>" +
                    "</size>" +
                "</color>";
            GUIContent content = new GUIContent(richTextMarkup);
            GUI.Label(position, content, style);
        }

        public override float GetHeight()
        {
            GUIContent content = new GUIContent(coloredHeaderAttribute.header);
            float fullTextHeight = EditorStyles.boldLabel.CalcHeight(content, 1.0f);
            
            int lines = 1;
            if (coloredHeaderAttribute.header != null)
                lines = coloredHeaderAttribute.header.Count(character => character == '\n') + 1;

            float eachLineHeight = fullTextHeight / lines;
            return EditorGUIUtility.singleLineHeight * 1.5f + (eachLineHeight * (lines - 1));
        }
    }
}