using UnityEngine;
using UnityEngine.UI;

namespace SingleUseWorld
{
    public class CircularCurtain : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvasRectTransform;
        [SerializeField] private RectTransform _curtainRectTransform;
        [SerializeField] private Material _curtainMaterial;

        [SerializeField] private string _centerXPropName = "_CenterX";
        [SerializeField] private string _centerYPropName = "_CenterY";
        [SerializeField] private string _radiusPropName = "_Radius";

        private int _centerXPropID = 0;
        private int _centerYPropID = 0;
        private int _radiusPropID = 0;

        private void Start()
        {
            CacheMaterialProperties();
            ResetMaterialProperties();
            ScaleToLargestSide();
        }

        public void SetRadius(float radius)
        {
            _curtainMaterial.SetFloat(_radiusPropID, radius);
        }

        public void SetCenter(Vector2 screenPoint)
        {
            var uvPoint = ScreenToUVPoint(screenPoint);

            _curtainMaterial.SetFloat(_centerXPropID, uvPoint.x);
            _curtainMaterial.SetFloat(_centerYPropID, uvPoint.y);
        }

        private void ScaleToLargestSide()
        {
            var canvasRect = _canvasRectTransform.rect;
            var largestSide = Mathf.Max(canvasRect.width, canvasRect.height);

            _curtainRectTransform.sizeDelta = new Vector2(largestSide, largestSide);
        }

        private Vector2 ScreenToUVPoint(Vector2 screenPoint)
        {
            var canvasRectSize = _canvasRectTransform.rect.size;
            var imageRectSize = _curtainRectTransform.rect.size;

            var differenceOffset = Vector2Util.Abs(canvasRectSize - imageRectSize) * 0.5f;
            var uvPoint = (screenPoint + differenceOffset) / imageRectSize;
            
            return uvPoint;
        }

        private void CacheMaterialProperties()
        {
            _centerXPropID = Shader.PropertyToID(_centerXPropName);
            _centerYPropID = Shader.PropertyToID(_centerYPropName);
            _radiusPropID = Shader.PropertyToID(_radiusPropName);
        }

        private void ResetMaterialProperties()
        {
            _curtainMaterial.SetFloat(_radiusPropID, 0f);
            _curtainMaterial.SetFloat(_centerXPropID, 0.5f);
            _curtainMaterial.SetFloat(_centerYPropID, 0.5f);
        }
    }
}