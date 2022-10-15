using SingleUseWorld.FSM;
using System;
using UnityEngine;


namespace SingleUseWorld
{
    public sealed class Player : BaseCharacter, IControllable, IGrabbable
    {
        #region Fields
        [SerializeField] private PlayerArmament _armament = default;
        [SerializeField] private PlayerBodyView _body = default;
        [SerializeField] private ShadowView _shadow = default;

        private PlayerSettings _settings;
        private PlayerHealth _health;
        private PlayerSpeed _speed;
        private PlayerGripHandler _gripHandler;
        #endregion

        #region LifeCycle Methods
        private void Update()
        {
            _gripHandler.Tick();
        }
        #endregion

        #region Public Methods
        public void OnCreate(PlayerSettings settings)
        {
            _settings = settings;
            _speed = new PlayerSpeed(_settings.SpeedSettings, _movement);
            _health = new PlayerHealth(_settings.HealthSettings, this);
            _gripHandler = new PlayerGripHandler(_settings.GripHandlerSettings, _speed, _health);

            _armament.Initialize(_settings.ArmamentSettings);

            _armament.StateChanged += OnArmamentStateChanged;
            _movement.StateChanged += OnMovementStateChanged;
            _projectile.GroundCollision += OnGroundHit;
            _health.Died += OnDied;
        }

        public void OnDestroy()
        {
            _armament.StateChanged -= OnArmamentStateChanged;
            _movement.StateChanged -= OnMovementStateChanged;
            _projectile.GroundCollision -= OnGroundHit;
            _health.Died -= OnDied;
        }

        void IControllable.StartMovement()
        {
            _movement.StartMovement();
        }

        void IControllable.SetMovementDirection(Vector2 direction)
        {
            _movement.SetDirection(direction);
            _body.SetFacingDirection(direction);
        }

        void IControllable.StopMovement()
        {
            _movement.SetDirection(Vector2.zero);
            _movement.StopMovement();
        }

        void IControllable.SetArmamentDirection(Vector2 direction)
        {
            _armament.SetDirection(direction);
        }

        void IControllable.Use()
        {
            if (_armament.State == ArmamentState.Unarmed)
                return;

            _armament.Use();
        }

        void IControllable.Drop()
        {
            _armament.Drop();
        }

        void IGrabbable.GrabbedBy(IGrabber grabInstigator)
        {
            _gripHandler.GrabbedBy(grabInstigator);
        }

        void IGrabbable.ReleasedBy(IGrabber grabInstigator)
        {
            _gripHandler.ReleasedBy(grabInstigator);
        }
        #endregion

        #region Private Methods
        private void OnDied()
        {
            _armament.PickupAllowed = false;
            _gripHandler.Reset();

            var damage = GenerateRandomDamage();
            _movement.Knockback(damage.horizontalKnockback, damage.verticalKnockback);
            _body.SetFacingDirection(damage.direction);
            _body.StartSpin(damage.spinKnockback);
            _body.ShowFlash(0.1f);

            var layer = LayerMask.NameToLayer(PhysicsLayer.KnockedCharacter.ToString());
            gameObject.layer = layer;
            _projectile.SetLayer(layer);
        }

        private Damage GenerateRandomDamage()
        {
            // damage
            var damageAmount = 0f;
            var damageDirection = _movement.FacingDirection;

            // knockback
            var verticalKnockback = 4f;
            var horizontalKnockback = damageDirection * 4f;
            var spinKnockback = -Mathf.Sign(damageDirection.x) * 90f;

            return new Damage(damageAmount, damageDirection, horizontalKnockback, verticalKnockback, spinKnockback);
        }

        private void OnGroundHit()
        {
            if (_health.IsDead)
                Destroy(gameObject);
        }

        private void OnMovementStateChanged(MovementState movementState)
        {
            switch (movementState)
            {
                case MovementState.Knocked:
                    _body.PlayKnockedAnimation();
                    elevator.height = _settings.KnockbackInitialHeight;
                    break;
                default:
                    ResolveAnimation();
                    break;
            }
        }

        private void OnArmamentStateChanged(ArmamentState armamentState)
        {
            switch (armamentState)
            {
                default:
                    ResolveAnimation();
                    break;
            }
        }

        private void ResolveAnimation()
        {
            switch (_armament.State)
            {
                case ArmamentState.Unarmed:
                    _speed.ResetItemFactor();
                    ResolveUnarmedMovement();
                    break;
                case ArmamentState.Armed:
                    _speed.SetItemFactor(_armament.HeldItem.SpeedFactor);
                    ResolveArmedMovement();
                    break;
            }
        }

        private void ResolveUnarmedMovement()
        {
            switch (_movement.State)
            {
                case MovementState.Idling:
                    _body.PlayIdleUnarmedAnimation();
                    break;
                case MovementState.Moving:
                    _body.PlayMoveUnarmedAnimation();
                    break;
            }
        }

        private void ResolveArmedMovement()
        {
            switch (_movement.State)
            {
                case MovementState.Idling:
                    _body.PlayIdleArmedAnimation();
                    break;
                case MovementState.Moving:
                    _body.PlayMoveArmedAnimation();
                    break;
            }
        }
        #endregion
    }
}