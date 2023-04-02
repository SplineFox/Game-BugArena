#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using UnityEngine;
using System.Collections.Generic;

namespace BugArena
{
    [RequireComponent(typeof(Elevator), typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class Projectile2D : MonoBehaviour
    {
        #region Constants
        // Specifies a small offset from colliders to ensure we don't try to get too close.
        // Moving too close can mean we get hits when moving tangential to a surface which results
        // in the object not being able to move.
        private const float SAFE_DISTANCE = 0.04f;
        
        // Specifies the number of iterations to detect and resolve physical collisions.
        private const int DETECTION_ITERATIONS = 2;

        // Specifies the epsilon value under which any movement distance or direction will be considered zero.
        private const float DISTANCE_THRESHOLD = 0.0048f;
        
        // Specifies the threshold under which velocity will not be taken into account.
        private const float VELOCITY_THRESHOLD = 0.0064f;
        #endregion

        #region Fields
        private Elevator _elevator = default;
        private Rigidbody2D _rigidbody2D = default;
        private CircleCollider2D _circleCollider2D = default;

        private Vector2 _horizontalVelocity = Vector2.zero;
        private float _verticalVelocity = 0f;

        private bool _wasGrounded = false;
        private bool _grounded = false;

        private ContactFilter2D _contactFilter = default;

        [SerializeField]
        [Tooltip("Controls whether physics affects the projectile.")]
        private bool _isKinematic = false;

        [SerializeField]
        [Tooltip("Controls whether the projectile should ricochet when hitting a wall.")]
        private bool _shouldRikochet = true;

        [SerializeField]
        [Min(0f)]
        [Tooltip("Specifies the value of the force that pulls the projectile to the ground.")]
        private float _gravityScale = 10f;
        #endregion

        #region Properties
        public Vector2 HorizontalVelocity { get => _horizontalVelocity; }
        public float VerticalVelocity { get => _verticalVelocity; }
        public float GravityScale { get => _gravityScale; set => _gravityScale = Mathf.Max(0f, value); }
        public bool IsKinematic { get => _isKinematic; set => _isKinematic = value; }
        public bool ShouldRikochet { get => _shouldRikochet; set => _shouldRikochet = value; }
        #endregion

        #region Delegates & Events
        public event Action WallCollision = delegate { };
        public event Action GroundCollision = delegate { };
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            _elevator = GetComponent<Elevator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();

            _rigidbody2D.isKinematic = true;
            _rigidbody2D.freezeRotation = true;

            _grounded = _elevator.grounded;
            _wasGrounded = _grounded;

            _contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            _contactFilter.useLayerMask = true;
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            HandleHorizontalMovement(fixedDeltaTime);
            HandleVerticalMovement(fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            RemoveOverlap(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            RemoveOverlap(collision);
        }

        private void RemoveOverlap(Collision2D collision)
        {
            // Check if collision should be resolved
            if (_contactFilter.IsFilteringLayerMask(collision.collider.gameObject))
                return;

            var collisionDistance = Physics2D.Distance(collision.otherCollider, collision.collider);
            if (collisionDistance.isOverlapped)
                collision.otherRigidbody.position += collisionDistance.normal * collisionDistance.distance;
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

        public void SetLayer(int layer)
        {
            _contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(layer));
        }
        #endregion

        #region Private Methods
        private void HandleHorizontalMovement(float fixedDeltaTime)
        {
            if (IsHorizontalVelocityUnderThreshold())
                return;

            var speed = _horizontalVelocity.magnitude;
            var distance = speed * fixedDeltaTime;
            var direction = _horizontalVelocity.normalized;
            var position = _rigidbody2D.position;

            if (_isKinematic)
                position = MoveAndSlide(position, direction, distance);
            else
                position = MoveAndCollide(position, direction, distance);

            _rigidbody2D.position = position;
        }

        private RaycastHit2D DetectCollision(Vector2 position, Vector2 direction, float distance)
        {
            var hit = new RaycastHit2D[1];

            if (_circleCollider2D.Cast(direction, _contactFilter, hit, distance + SAFE_DISTANCE, true) > 0)
                hit[0].distance = hit[0].distance - SAFE_DISTANCE;

            return hit[0];
        }

        private Vector2 MoveAndCollide(Vector2 position, Vector2 direction, float distance)
        {
            // Detect
            var collision = DetectCollision(position, direction, distance);
            if(collision)
            {
                // Resolve
                distance = collision.distance;

                // Response
                HandleWallCollision(collision.normal);
            }

            position += direction * distance;
            return position;
        }

        private Vector2 MoveAndSlide(Vector2 position, Vector2 direction, float distance)
        {
            var iteration = 0;

            while (iteration++ < DETECTION_ITERATIONS &&
                   distance > DISTANCE_THRESHOLD &&
                   direction.sqrMagnitude > DISTANCE_THRESHOLD)
            {
                // Detect
                var collision = DetectCollision(position, direction, distance);
                if(collision)
                {
                    // Resolve
                    position += direction * collision.distance;
                    distance -= collision.distance;
                    // Response
                    direction -= collision.normal * Vector2.Dot(direction, collision.normal);
                }
                else
                {
                    position += direction * distance;
                    break;
                }
            }

            return position;
        }

        private void HandleVerticalMovement(float fixedDeltaTime)
        {
            if (_isKinematic || _grounded)
                return;

            ApplyGravity(fixedDeltaTime);
            CheckGroundCollision();
        }

        private void ApplyGravity(float fixedDeltaTime)
        {
            var gravityForce = GravityScale * fixedDeltaTime;
            _verticalVelocity -= gravityForce;

            // Apply vertical movement.
            var verticalTranslation = _verticalVelocity * fixedDeltaTime;
            _elevator.Translate(verticalTranslation);
        }

        private bool IsHorizontalVelocityUnderThreshold()
        {
            return (_horizontalVelocity.sqrMagnitude) < VELOCITY_THRESHOLD;
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
            if (_shouldRikochet)
                _horizontalVelocity = Vector2.Reflect(_horizontalVelocity, collisionNormal);
            else
                _horizontalVelocity = Vector2.zero;

            WallCollision.Invoke();
        }

        private void HandleGroundCollision()
        {
            _verticalVelocity = 0f;
            _horizontalVelocity = Vector2.zero;

            GroundCollision.Invoke();
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            DrawDirection();
        }

        private void DrawDirection()
        {
            if (_horizontalVelocity.magnitude != 0)
            {
                Gizmos.color = Color.blue;
                GizmosUtil.DrawArrow(transform.position, _horizontalVelocity.normalized);
                Gizmos.color = Color.white;
            }
        }
#endif
    }
}