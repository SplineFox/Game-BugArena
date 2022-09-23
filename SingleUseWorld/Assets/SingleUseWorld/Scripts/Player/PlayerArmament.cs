using System;
using UnityEngine;

namespace SingleUseWorld
{
    public enum ArmamentState
    {
        Unarmed,
        Armed,
        Throwed
    }

    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerArmament : BaseComponent<ArmamentState>
    {
        #region Fields
        [SerializeField]
        private bool _pickupAllowed = true;
        private Cooldown _pickupCooldown = default;
        private float _pickupCooldownTime = 0.5f;

        private Vector2 _direction = Vector2.right;
        private float _attachmentHeight = 1.8f;

        private Collider2D _collider2D = default;
        private Item _item = default;
        #endregion

        #region Properties
        public Item HeldItem { get => _item; }
        public Vector2 AimDirection { get => _direction; }
        public bool PickupAllowed { get => _pickupAllowed; set => _pickupAllowed = value; }
        #endregion

        #region LifeCycle Methods
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            _pickupCooldown.Update(deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TryPickUp(collision);
        }
        #endregion

        #region Public Methods
        public override void Initialize()
        {
            _state = ArmamentState.Unarmed;

            _collider2D = GetComponent<Collider2D>();

            _pickupCooldown = new Cooldown(_pickupCooldownTime);
            _pickupCooldown.Completed += CheckTrigger2D;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void PickUp(Item item)
        {
            if (!_pickupAllowed || !_pickupCooldown.IsCompleted)
                return;

            if (_item != null)
            {
                Switch(item);
                return;
            }

            _item = item;
            _item.Attach(transform, _attachmentHeight);

            SetState(ArmamentState.Armed);
        }

        public void Drop()
        {
            if (_item == null)
                return;

            _item.Detach();
            _item = null;

            _pickupCooldown.Start();
            SetState(ArmamentState.Unarmed);
        }

        public void Use()
        {
            if (_item == null)
                return;

            _item.Use(_direction, gameObject);
            _item = null;

            _pickupAllowed = false;
            SetState(ArmamentState.Throwed);
        }

        public void FinishThrowedState()
        {
            if (_state != ArmamentState.Throwed)
                return;

            _pickupAllowed = true;
            SetState(ArmamentState.Unarmed);
        }
        #endregion

        #region Private Methods
        private void Switch(Item item)
        {
            _item.Detach();
            _item = item;
            _item.Attach(transform, _attachmentHeight);
            _pickupCooldown.Start();
        }

        private void CheckTrigger2D()
        {
            Collider2D[] collision = new Collider2D [1];
            var collisionCount = _collider2D.GetContacts(collision);

            if(collisionCount != 0)
            {
                TryPickUp(collision[0]);
            }
        }

        private void TryPickUp(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Item>(out var item))
            {
                PickUp(item);
            }
        }
        #endregion
    }
}