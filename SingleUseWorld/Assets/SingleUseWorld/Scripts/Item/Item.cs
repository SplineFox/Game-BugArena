using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Item : BaseBehaviour, IPoolable
    {
        #region Fields
        protected float _bobbingHeight = 0.0625f;
        protected float _bobbingSpeed = 8f;

        protected Collider2D _collider = default;
        protected Rigidbody2D _rigidbody = default;
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
            StartCoroutine(Bob());
        }
        #endregion

        #region Public Methods
        void IPoolable.Reset()
        {
            StopAllCoroutines();
            elevator.height = 0f;
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            StartCoroutine(Bob());
        }

        public void Attach(Transform target, float height)
        {
            // attach to parent
            transform.SetParent(target);

            // play raise animation
            StopAllCoroutines();
            StartCoroutine(MoveTo(Vector3.zero));
            StartCoroutine(ElevateTo(height));

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
            StartCoroutine(ElevateTo(0f));

            // enable collisions
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        public abstract void Use(Vector2 direction);
        #endregion

        #region Private Methods
        private IEnumerator ElevateTo(float targetHeight)
        {
            targetHeight = Mathf.Max(targetHeight, 0f);
            while(Mathf.Abs(elevator.height - targetHeight) > 0.05f)
            {
                elevator.height = Mathf.Lerp(elevator.height, targetHeight, Time.deltaTime * 10f);
                yield return null;
            }
            elevator.height = targetHeight;

            // if item became detached
            if (transform.parent == null)
            {
                StartCoroutine(Bob());
            }
        }

        private IEnumerator MoveTo(Vector3 targetPosition)
        {
            while(Vector3.Distance(transform.localPosition, targetPosition) > 0.05f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 10f);
                yield return null;
            }
            transform.localPosition = targetPosition;
        }

        private IEnumerator Bob()
        {
            float bobbingProgress = 0f;
            float sinShift = Mathf.PI / 2;
            while (true)
            {
                bobbingProgress += _bobbingSpeed * Time.deltaTime;
                elevator.height = Mathf.Sin(bobbingProgress - sinShift) * _bobbingHeight + _bobbingHeight;
                yield return null;
            }
        }
        #endregion
    }
}