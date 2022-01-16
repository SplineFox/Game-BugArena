namespace UnityEngine
{
    public static class GizmosExtension
    {
        public static void DrawArrow(Vector2 from, Vector2 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Vector2 rightArrowHead = Quaternion.Euler(0, 0, 180 + arrowHeadAngle) * to.normalized;
            Vector2 leftArrowHead = Quaternion.Euler(0, 0, 180 - arrowHeadAngle) * to.normalized;
            
            Gizmos.DrawRay(from, to);
            Gizmos.DrawRay(from + to, rightArrowHead * arrowHeadLength);
            Gizmos.DrawRay(from + to, leftArrowHead * arrowHeadLength);
        }

        public static void DrawLineSegment(Vector2 from, Vector2 to, float serifLength = 0.25f)
        {
            Vector2 lineDirection = (to - from).normalized;
            Vector2 serifDirection = Vector2.Perpendicular(lineDirection);
            Vector2 serifOffset = serifDirection * serifLength / 2;
            
            Gizmos.DrawLine(from, to);
            Gizmos.DrawRay(from - serifOffset, serifDirection * serifLength);
            Gizmos.DrawRay(to - serifOffset, serifDirection * serifLength);
        }
    }
}