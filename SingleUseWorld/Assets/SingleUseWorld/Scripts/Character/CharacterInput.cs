using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

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
        [SerializeField] private Vector2 _lookInput = Vector2.zero;
        #endregion

        #region Properties
        public Vector2 MoveInput { get => _moveInput; }
        public Vector2 LookInput { get => _lookInput; }
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            Assert.IsNotNull(_inputReaderSO, "\"InputReaderSO\" is required.");
        }

        private void OnEnable()
        {
            _inputReaderSO.MoveEvent += OnMoveInput;
            _inputReaderSO.LookEvent += OnLookInput;
        }

        private void OnDisable()
        {
            _inputReaderSO.MoveEvent -= OnMoveInput;
            _inputReaderSO.LookEvent -= OnLookInput;
        }
        #endregion

        #region Private Methods
        private void OnMoveInput(Vector2 moveInput)
        {
            _moveInput = moveInput;
        }

        private void OnLookInput(Vector2 lookInput)
        {
            _lookInput = lookInput;
        }
        #endregion
    }
}