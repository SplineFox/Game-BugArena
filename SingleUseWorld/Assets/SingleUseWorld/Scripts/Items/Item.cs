using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Item : BaseBehaviour, IPoolable
    {
        #region Fields
        [SerializeField] private ItemBodyView _body = default;

        private Collider2D _collider = default;
        private Rigidbody2D _rigidbody = default;

        private IMonoFactory<ItemEntity> _entityFactory = default;
        private ItemSettings _settings = default;
        private ItemTypeSettings _typeSettings = default;
        #endregion

        #region Properties
        public ItemType Type 
        { 
            get => _typeSettings.Type; 
        }

        public float SpeedFactor
        {
            get => _typeSettings.SpeedFactor;
        }
        #endregion

        #region Delegates & Events
        public event Action<Item> Used = delegate { };
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StartCoroutine(Bob(_settings.BobbingHeight, _settings.BobbingSpeed));
        }
        #endregion

        #region Public Methods
        public void OnCreate(ItemSettings settings, ItemTypeSettings typeSettings, IMonoFactory<ItemEntity> entityFactory)
        {
            _settings = settings;
            _typeSettings = typeSettings;
            _entityFactory = entityFactory;
        }

        public void OnDestroy()
        { }

        void IPoolable.OnReset()
        {
            StopAllCoroutines();
            transform.SetParent(null);
            transform.Reset();
            elevator.Reset();
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            StartCoroutine(Bob(_settings.BobbingHeight, _settings.BobbingSpeed));
        }

        public void Attach(Transform target, float height)
        {
            // attach to parent
            transform.SetParent(target);
            transform.rotation = target.rotation;

            // play raise animation
            StopAllCoroutines();
            StartCoroutine(MoveTo(Vector3.zero, _settings.MovementSpeed));
            StartCoroutine(ElevateTo(height, _settings.ElevationSpeed));

            // disable collisions
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
        }

        public void Detach()
        {
            // detach from parent
            transform.SetParent(null);

            // play lower animation
            StopAllCoroutines();
            StartCoroutine(ElevateTo(0f, _settings.ElevationSpeed));
            StartCoroutine(RotateTo(Quaternion.identity, 10f));

            // enable collisions
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        public void Use(Vector2 direction, GameObject instigator)
        {
            var entity = _entityFactory.Create();
            entity.transform.position = this.transform.position;
            entity.Use(direction, transform.parent.gameObject);
            Used.Invoke(this);
        }
        #endregion

        #region Private Methods
        private IEnumerator ElevateTo(float targetHeight, float elevationSpeed)
        {
            targetHeight = Mathf.Max(targetHeight, 0f);
            while(Mathf.Abs(elevator.height - targetHeight) > 0.05f)
            {
                elevator.height = Mathf.Lerp(elevator.height, targetHeight, Time.deltaTime * elevationSpeed);
                yield return null;
            }
            elevator.height = targetHeight;

            // if item became detached
            if (transform.parent == null)
            {
                StartCoroutine(Bob(_settings.BobbingHeight, _settings.BobbingSpeed));
            }
        }

        private IEnumerator MoveTo(Vector3 targetPosition, float movementSpeed)
        {
            while(Vector3.Distance(transform.localPosition, targetPosition) > 0.05f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * movementSpeed);
                yield return null;
            }
            transform.localPosition = targetPosition;
        }

        private IEnumerator RotateTo(Quaternion targetRotation, float rotationSpeed)
        {
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.05f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                yield return null;
            }
            transform.rotation = targetRotation;
        }

        private IEnumerator Bob(float bobbingHeight, float bobbingSpeed)
        {
            float bobbingProgress = 0f;
            float sinShift = Mathf.PI / 2;
            while (true)
            {
                bobbingProgress += bobbingSpeed * Time.deltaTime;
                elevator.height = Mathf.Sin(bobbingProgress - sinShift) * bobbingHeight + bobbingHeight;
                yield return null;
            }
        }
        #endregion
    }
}