using System;
using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "New TestGraph", menuName = "SingleUseWorld/Test/Create TestGraph")]
    public class TestGraph : ScriptableObject
    {
        [SerializeField] public TestSO so;
        [SerializeField] public List<TestNode> testNodes = new List<TestNode>();

        [ContextMenu(itemName: "AddTestNode")]
        public void AddTestNode()
        {
            Vector2 position = new Vector2();
            Color color = Color.blue;
            TestNode node = new TestNode(position, color, so);
            testNodes.Add(node);
        }
    }

    [Serializable]
    public class TestNode
    {
        [SerializeField]
        private Vector2 _position;

        [SerializeField]
        private Color _color;

        [SerializeField]
        private TestSO _testSO;

        public TestNode(Vector2 position, Color color, TestSO so)
        {
            _position = position;
            _color = color;
            _testSO = so;
        }
    }
}