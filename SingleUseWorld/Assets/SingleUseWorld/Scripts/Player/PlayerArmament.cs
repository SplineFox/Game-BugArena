using System;
using UnityEngine;

namespace SingleUseWorld
{
    public enum ArmamentState
    {
        Unarmed,
        Armed
    }

    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerArmament : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private bool _pickupAllowed = true;
        private Cooldown _pickupCooldown = default;

        private ArmamentState _state = default;
        private Vector2 _direction = Vector2.right;

        private Collider2D _collider2D = default;
        private Player _player = default;
        private Item _item = default;
        #endregion

        #region Properties
        public ArmamentState State { get => _state; }
        public bool PickupAllowed { get => _pickupAllowed; set => _pickupAllowed = value; }
        #endregion

        #region Delegates & Events
        public Action<ArmamentState> StateChanged = delegate { };
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
        public void Initialize(Player player)
        {
            _player = player;
            _state = ArmamentState.Unarmed;

            _collider2D = GetComponent<Collider2D>();

            _pickupCooldown = new Cooldown(2f);
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
                Drop();

            _item = item;
            _item.Attach(transform, 1.8f);

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

            _item.Use(_direction);
            _item = null;
            SetState(ArmamentState.Unarmed);
        }
        #endregion

        #region Private Methods
        private void SetState(ArmamentState state)
        {
            if (_state == state)
                return;

            _state = state;
            StateChanged.Invoke(state);
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