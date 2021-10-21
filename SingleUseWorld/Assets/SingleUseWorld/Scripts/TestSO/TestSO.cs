using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "New TestSO", menuName = "SingleUseWorld/TestSO/Create")]
    public class TestSO : ScriptableObject
    {
        [SerializeField] private float floatField = 1.0f;
        [SerializeField] private Color colorField = Color.red;

        public float floatProperty
        {
            get => floatField;
            set
            {
                floatField = value;
                Debug.Log("floatProperty has changed: " + floatField);
            }
        }

        public Color colorProperty
        { 
            get => colorField;
            set
            {
                colorField = value;
                Debug.Log("colorProperty has changed: " + colorField);
            }
        }

        public void NotifyHasChanged()
        {
            NotifyFloatHasChanged();
            NotifyColorHasChanged();
        }

        public void NotifyFloatHasChanged()
        {
            Debug.Log("floatField has changed: " + floatField);
        }

        public void NotifyColorHasChanged()
        {
            Debug.Log("colorField has changed: " + colorField);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            NotifyHasChanged();
        }
#endif
    }
}