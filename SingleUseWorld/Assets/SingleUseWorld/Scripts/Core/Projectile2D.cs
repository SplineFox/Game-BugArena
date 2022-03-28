#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Elevator), typeof(Rigidbody2D))]
    public class Projectile2D : MonoBehaviour
    {
        #region Fields
        private Elevator _elevator = default;
        private Rigidbody2D _rigidbody2D = default;

        private Vector2 _horizontalVelocity = Vector2.zero;
        private float _verticalVelocity = 0f;

        private bool _wasGrounded = false;
        private bool _grounded = false;

        [SerializeField]
        private bool _isKinematic = false;

        [SerializeField]
        [Min(0f)]
        private float _gravityScale = 10f;

        [SerializeField]
        [Range(0f, 1f)]
        private float _frictionScale = 0.4f;

        [SerializeField]
        [Range(0f, 1f)]
        private float _bounceScale = 0.6f;

        [Space]
        [SerializeField]
        private bool _shouldBounce = true;

        [SerializeField]
        [Min(0f)]
        private float _bounceVelocityThreshold = 0.08f;

        [SerializeField]
        [Min(0f)]
        private float _moveVelocityThreshold = 0.08f;
        #endregion

        #region Properties
        public Vector2 HorizontalVelocity { get => _horizontalVelocity; }
        public float VerticalVelocity { get => _verticalVelocity; }
        public float GravityScale { get => _gravityScale; set => _gravityScale = Mathf.Max(0f, value); }
        public float FrictionScale { get => _frictionScale; set => _frictionScale = Mathf.Clamp01(value); }
        public float BounceScale { get => _bounceScale; set => _bounceScale = Mathf.Clamp01(value); }
        public bool ShouldBounce { get => _shouldBounce; set => _shouldBounce = value; }
        public float BounceVelocityThreshold { get => _bounceVelocityThreshold; set => _bounceVelocityThreshold = Mathf.Max(0f, value); }
        public float MoveVelocityThreshold { get => _moveVelocityThreshold; set => _moveVelocityThreshold = Mathf.Max(0f, value); }
        public bool IsKinematic { get => _isKinematic; set => _isKinematic = value; }
        #endregion

        #region Delegates & Events
        public event Action Bounced = delegate { };
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            _elevator = GetComponent<Elevator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _grounded = _elevator.grounded;
            _wasGrounded = _grounded;
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;

            HandlePhysics(fixedDeltaTime);
            ApplyMovement(fixedDeltaTime);
            CheckGroundCollision();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var collisionNormal = collision.GetContact(0).normal;
            HandleWallCollision(collisionNormal);
        }
        #endregion

        #region Public Methods
        public void SetVelocity(Vector2 horizontalVelocity, float verticalVelocity = 0f)
        {
            _horizontalVelocity = horizontalVelocity;
            _verticalVelocity = verticalVelocity;

            if (!_isKinematic)
            {
                _grounded = false;
                _wasGrounded = _grounded;
            }
        }

        public void ResetVelocity()
        {
            SetVelocity(Vector2.zero, 0f);
        }
        #endregion

        #region Private Methods
        private void HandlePhysics(float fixedDeltaTime)
        {
            if (_isKinematic)
                return;

            if (_grounded)
            {
                HandleSliding(fixedDeltaTime);
            }
            else
            {
                HandleFlying(fixedDeltaTime);
            }
        }

        private void HandleSliding(float fixedDeltaTime)
        {
            ApplyFriction(fixedDeltaTime);

            if (IsHorizontalVelocityUnderThreshold())
            {
                _horizontalVelocity = Vector2.zero;
            }
        }

        private void HandleFlying(float fixedDeltaTime)
        {
            ApplyGravity(fixedDeltaTime);
        }

        private void ApplyFriction(float fixedDeltaTime)
        {
            var normalForce = GravityScale * fixedDeltaTime;
            var frictionForce = HorizontalVelocity.normalized * FrictionScale * normalForce;
            _horizontalVelocity -= frictionForce;
        }

        private void ApplyGravity(float fixedDeltaTime)
        {
            var gravityForce = GravityScale * fixedDeltaTime;
            _verticalVelocity -= gravityForce;
        }

        private bool IsVerticalVelocityUnderThreshold()
        {
            var verticalVelocitySqr = VerticalVelocity * VerticalVelocity;
            var bounceVelocitySqr = BounceVelocityThreshold * BounceVelocityThreshold;
            return verticalVelocitySqr < bounceVelocitySqr;
        }

        private bool IsHorizontalVelocityUnderThreshold()
        {
            var horizontalVelocitySqr = HorizontalVelocity.sqrMagnitude;
            var moveVelocitySqr = MoveVelocityThreshold * MoveVelocityThreshold;
            return horizontalVelocitySqr < moveVelocitySqr;
        }

        private void CheckGroundCollision()
        {
            _wasGrounded = _grounded;
            _grounded = _elevator.grounded;

            if (!_wasGrounded && _grounded)
            {
                HandleGroundCollision();
            }
        }

        private void HandleWallCollision(Vector2 collisionNormal)
        {
            if (_isKinematic)
                return;

            if (ShouldBounce)
            {
                _horizontalVelocity = Vector2.Reflect(_horizontalVelocity, collisionNormal);
                _horizontalVelocity *= BounceScale;
            }
        }

        private void HandleGroundCollision()
        {
            if (_isKinematic)
                return;

            if (ShouldBounce)
            {
                var newVerticalVelocity = -VerticalVelocity * BounceScale;
                var newHorizontalVelocity = HorizontalVelocity * (1f - FrictionScale);

                if (IsHorizontalVelocityUnderThreshold())
                {
                    _horizontalVelocity = Vector2.zero;
                }
                if (IsVerticalVelocityUnderThreshold())
                {
                    _verticalVelocity = 0f;
                    return;
                }

                SetVelocity(newHorizontalVelocity, newVerticalVelocity);

                Bounced.Invoke();
            }
            else
            {
                _verticalVelocity = 0f;
                _horizontalVelocity = Vector2.zero;
            }
        }

        private void ApplyMovement(float fixedDeltaTime)
        {
            Vector2 horizontalTranslation = HorizontalVelocity * fixedDeltaTime;
            float verticalTranslation = VerticalVelocity * fixedDeltaTime;

            Vector2 newPosition = _rigidbody2D.position + horizontalTranslation;

            _rigidbody2D.MovePosition(newPosition);
            _elevator.Translate(verticalTranslation);
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            DrawDirection();
        }

        private void DrawDirection()
        {
            if (HorizontalVelocity.magnitude != 0)
            {
                Gizmos.color = Color.blue;
                GizmosExtension.DrawArrow(transform.position, HorizontalVelocity.normalized);
                Gizmos.color = Color.white;
            }
        }
#endif
    }
}