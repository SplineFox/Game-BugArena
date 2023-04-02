using System;
using System.Collections;
using UnityEngine;

namespace BugArena
{
    public enum ArmamentState
    {
        Unarmed,
        Armed
    }

    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerArmament : BaseComponent<ArmamentState>
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float PickupCooldownTime = 0.5f;
            public float AttachmentHeight = 1.8f;
        }
        #endregion

        #region Fields
        [SerializeField]
        private Settings _settings;
        private bool _pickupAllowed = true;
        private Coroutine _pickupCooldownCoroutine = default;
        private Vector2 _direction = Vector2.right;

        private Collider2D _collider2D = default;
        private Item _item = default;
        #endregion

        #region Properties
        public Item HeldItem { get => _item; }
        public Vector2 AimDirection { get => _direction; }
        public bool PickupAllowed 
        { 
            get => _pickupAllowed;
            set
            {
                if (_pickupAllowed == value)
                    return;

                _pickupAllowed = value;
                if (_pickupAllowed)
                    EnableTrigger();
                else
                    DisableTrigger();
            }
        }

        private bool PickupCooldownCompleted
        {
            get => _pickupCooldownCoroutine == null;
        }
        #endregion

        #region LifeCycle Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TryPickUp(collision);
        }
        #endregion

        #region Public Methods
        public void Initialize(Settings settings)
        {
            _settings = settings;
            _state = ArmamentState.Unarmed;

            _collider2D = GetComponent<Collider2D>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void PickUp(Item item)
        {
            if (!PickupAllowed || !PickupCooldownCompleted)
                return;

            if (_item != null)
            {
                Switch(item);
                return;
            }

            _item = item;
            _item.Attach(transform, _settings.AttachmentHeight);

            SetState(ArmamentState.Armed);
        }

        public void Drop()
        {
            if (_item == null)
                return;

            _item.Detach();
            _item = null;

            StartPickupCooldown();
            SetState(ArmamentState.Unarmed);
        }

        public void Use()
        {
            if (_item == null)
                return;

            _item.Use(_direction, gameObject);
            _item = null;

            SetState(ArmamentState.Unarmed);
        }
        #endregion

        #region Private Methods
        private void Switch(Item item)
        {
            _item.Detach();
            _item = item;
            _item.Attach(transform, _settings.AttachmentHeight);
            StartPickupCooldown();
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

        private void StartPickupCooldown()
        {
            if (!PickupCooldownCompleted)
            {
                StopCoroutine(_pickupCooldownCoroutine);
            }
            _pickupCooldownCoroutine = StartCoroutine(PickupCooldown(_settings.PickupCooldownTime));
        }

        private IEnumerator PickupCooldown(float duration)
        {
            yield return new WaitForSeconds(duration);
            _pickupCooldownCoroutine = null;
            CheckTrigger2D();
        }

        private void EnableTrigger()
        {
            _collider2D.enabled = true;
        }

        private void DisableTrigger()
        {
            Drop();
            _collider2D.enabled = false;
        }
        #endregion
    }
}