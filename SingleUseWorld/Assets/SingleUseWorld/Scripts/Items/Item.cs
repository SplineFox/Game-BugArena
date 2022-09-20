using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Item : BaseBehaviour, IPoolable
    {
        #region Fields
        private float _bobbingHeight = 0.0625f;
        private float _bobbingSpeed = 8f;
        private float _elevationSpeed = 10f;
        private float _movementSpeed = 10f;

        private Collider2D _collider = default;
        private Rigidbody2D _rigidbody = default;

        private ItemType _itemType = default;
        private IMonoFactory<ItemEntity> _entityFactory = default;
        #endregion

        #region Properties
        public ItemType Type 
        { 
            get => _itemType; 
        }
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
            StartCoroutine(Bob(_bobbingHeight, _bobbingSpeed));
        }
        #endregion

        #region Public Methods
        public void OnCreate(ItemType itemType, IMonoFactory<ItemEntity> entityFactory)
        {
            _itemType = itemType;
            _entityFactory = entityFactory;
        }

        public void OnDestroy()
        { }

        void IPoolable.OnReset()
        {
            StopAllCoroutines();
            elevator.height = 0f;
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            StartCoroutine(Bob(_bobbingHeight, _bobbingSpeed));
        }

        public void Attach(Transform target, float height)
        {
            // attach to parent
            transform.SetParent(target);

            // play raise animation
            StopAllCoroutines();
            StartCoroutine(MoveTo(Vector3.zero, _movementSpeed));
            StartCoroutine(ElevateTo(height, _elevationSpeed));

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
            StartCoroutine(ElevateTo(0f, _elevationSpeed));

            // enable collisions
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        public void Use(Vector2 direction, GameObject instigator)
        {
            var entity = _entityFactory.Create();
            entity.transform.position = this.transform.position;
            entity.Use(direction, transform.parent.gameObject);
            Destroy(gameObject);
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
                StartCoroutine(Bob(_bobbingHeight, _bobbingSpeed));
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