using System;
using UnityEngine;

namespace BugArena
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class ColoredHeaderAttribute : PropertyAttribute
    {
        private const string defaulColorHTML = "#D3D3D3";

        public readonly string header;
        public readonly string colorHTML;

        public ColoredHeaderAttribute(string header, string colorHTML = defaulColorHTML)
        {
            this.header = header;

            if (ColorUtility.TryParseHtmlString(colorHTML, out var color))
                this.colorHTML = colorHTML;
            else
                this.colorHTML = defaulColorHTML;
        }
    }
}