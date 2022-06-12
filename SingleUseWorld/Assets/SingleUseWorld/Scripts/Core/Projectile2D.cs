#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using UnityEngine;
using System.Collections.Generic;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Elevator), typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class Projectile2D : MonoBehaviour
    {
        #region Constants
        // Specifies a small offset from colliders to ensure we don't try to get too close.
        // Moving too close can mean we get hits when moving tangential to a surface which results
        // in the object not being able to move.
        private const float HIT_OFFSET = 0.05f;
        
        // Specifies the number of iterations to detect and resolve physical collisions.
        private const int DETECTION_ITERATIONS = 2;

        // Specifies the epsilon value under which any movement distance or direction will be considered zero.
        private const float MOVEMENT_THRESHOLD = 0.005f;
        
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
        
        [SerializeField]
        [Tooltip("Specifies the layers to collide with.")]
        private LayerMask _collisionMask = default;
        #endregion

        #region Properties
        public Vector2 HorizontalVelocity { get => _horizontalVelocity; }
        public float VerticalVelocity { get => _verticalVelocity; }
        public float GravityScale { get => _gravityScale; set => _gravityScale = Mathf.Max(0f, value); }
        public bool IsKinematic { get => _isKinematic; set => _isKinematic = value; }
        public bool ShouldRikochet { get => _shouldRikochet; set => _shouldRikochet = value; }
        #endregion

        #region Delegates & Events
        public event Action GroundCollision = delegate { };
        public event Action WallCollision = delegate { };
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
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            HandleHorizontalMovement(fixedDeltaTime);
            HandleVerticalMovement(fixedDeltaTime);
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
        private void HandleHorizontalMovement(float fixedDeltaTime)
        {
            // Check velocity threshold.
            if (IsHorizontalVelocityUnderThreshold())
                return;

            // Movement parameters to adjust.
            var speed = _horizontalVelocity.magnitude;
            var distance = speed * fixedDeltaTime;
            var direction = _horizontalVelocity.normalized;
            var position = _rigidbody2D.position;
            
            var iteration = 0;
            RaycastHit2D hit = new RaycastHit2D();
            // During the first iteration, the presence of obstacles in the path of the initial movement is determined.
            // If an obstacle is detected, the direction and distance of movement are adjusted depending on the collision info.
            // During the second and subsequent iterations, the process is repeated for the adjusted movement.           
            while (
                iteration++ < DETECTION_ITERATIONS &&
                distance > MOVEMENT_THRESHOLD &&
                direction.sqrMagnitude > MOVEMENT_THRESHOLD
                )
            {
                var distanceAdjustment = distance;
                hit = Physics2D.CircleCast(position, _circleCollider2D.radius, direction, distance, _collisionMask);

                // If there was a hit.
                if (hit)
                {
                    // We only want to move if we are not too close to the collider.
                    if (hit.distance > HIT_OFFSET)
                    {
                        // Calculate adjusted distance.
                        distanceAdjustment = hit.distance - HIT_OFFSET;
                        // Adjust target position.
                        position += direction * distanceAdjustment;
                    }
                    else
                    {
                        // We had a hit but it resulted in us touching
                        // or being within allowed hit offset, so we
                        // don't need to adjust distance, therefore
                        // reset adjustment.
                        distanceAdjustment = 0f;
                    }

                    // Adjust direction based on hit normal.
                    // For tangential hits, the direction will be adjusted
                    // to slide along the collision.
                    direction -= hit.normal * Vector2.Dot(direction, hit.normal);

                    // Remove the distance we ended up moving from the initial.
                    distance -= distanceAdjustment;
                }
                else
                {
                    // No hit so move by the whole initial distance.
                    position += direction * distance;
                    break;
                }
            }

            // Apply horizontal movement.
            _rigidbody2D.MovePosition(position);
            // Check wall collision.
            if (hit)
                HandleWallCollision(hit.normal);
        }

        private void HandleVerticalMovement(float fixedDeltaTime)
        {
            // Check velocity threshold.
            if (_isKinematic || IsVericalVelocityUnderThreshold())
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

        private bool IsVericalVelocityUnderThreshold()
        {
            return (_verticalVelocity * _verticalVelocity) < VELOCITY_THRESHOLD;
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

            if (_shouldRikochet)
                _horizontalVelocity = Vector2.Reflect(_horizontalVelocity, collisionNormal);

            WallCollision.Invoke();
        }

        private void HandleGroundCollision()
        {
            if (_isKinematic)
                return;

            _verticalVelocity = 0f;

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
                GizmosExtension.DrawArrow(transform.position, _horizontalVelocity.normalized);
                Gizmos.color = Color.white;
            }
        }
#endif
    }
}