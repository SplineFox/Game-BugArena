using System.Collections;
using UnityEngine;

namespace BugArena
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class CharacterBodyView : BodyView
    {
        #region Fields
        [SerializeField] private Material _flashMaterial = default;
        protected Material _originalMaterial = default;

        protected SpriteRenderer _spriteRenderer = default;
        protected Coroutine _spinCoroutine = default;

        [SerializeField] protected float _rollSpeedPerSecond = 20f;
        [SerializeField] protected float _rollDegree = 10;
        protected Quaternion _rollRotation = Quaternion.identity;
        protected bool _rollEnabled = true;
        #endregion

        #region Properties
        #endregion

        #region LifeCycle Methods
        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalMaterial = _spriteRenderer.sharedMaterial;
        }

        protected virtual void Update()
        {
            UpdateRoll();
        }
        #endregion

        #region Public Methods
        public virtual void SetFacingDirection(Vector2 facingDirection)
        {
            _spriteRenderer.flipX = facingDirection.x < 0;

            var rollAngle = Vector2.Dot(facingDirection, Vector2.right) * _rollDegree;
            SetRoll(rollAngle);
        }

        public void ShowFlash(float duration)
        {
            StartCoroutine(Flash(duration));
        }

        public void StartSpin(float spinSpeedPerSecond)
        {
            _rollEnabled = false;
            StopSpin();
            _spinCoroutine = StartCoroutine(Spin(spinSpeedPerSecond));
        }

        public void StopSpin()
        {
            if (_spinCoroutine != null)
                StopCoroutine(_spinCoroutine);
        }

        public void OnReset()
        {
            transform.Reset();
        }
        #endregion

        #region Protected Methods
        protected void UpdateRoll()
        {
            if (!_rollEnabled)
                return;

            transform.rotation = Quaternion.Lerp(transform.rotation, _rollRotation, Time.deltaTime * _rollSpeedPerSecond);
        }

        protected void ResetRoll()
        {
            SetRoll(0f);
        }

        protected void SetRoll(float rollAngle)
        {
            _rollRotation = Quaternion.Euler(Vector3.forward * rollAngle);
        }

        protected IEnumerator Flash(float duration)
        {
            _spriteRenderer.material = _flashMaterial;
            yield return new WaitForSeconds(duration);
            _spriteRenderer.material = _originalMaterial;
        }

        protected IEnumerator Spin(float spinSpeedPerSecond)
        {
            while (true)
            {
                var spinDelta = spinSpeedPerSecond * Time.deltaTime * Vector3.forward;
                transform.Rotate(spinDelta);
                yield return null;
            }
        }
        #endregion
    }
}