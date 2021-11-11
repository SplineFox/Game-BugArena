using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    /// <summary>
    /// Represents character input cache.
    /// </summary>
    public class CharacterInput : MonoBehaviour
    {
        #region Fields
        [SerializeField] private InputReaderSO _inputReaderSO = default;
        [SerializeField] private Vector2 _moveInput = Vector2.zero;
        #endregion

        #region Properties
        public Vector2 MoveInput { get => _moveInput; }
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            Assert.IsNotNull(_inputReaderSO, "\"InputReaderSO\" is required.");
        }

        private void OnEnable()
        {
            _inputReaderSO.MoveEvent += OnMoveInput;
        }

        private void OnDisable()
        {
            _inputReaderSO.MoveEvent -= OnMoveInput;
        }
        #endregion

        #region Private Methods
        private void OnMoveInput(Vector2 moveInput)
        {
            _moveInput = moveInput;
        }
        #endregion
    }
}