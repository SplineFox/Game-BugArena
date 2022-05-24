using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public abstract class Item : MonoBehaviour
    {
        #region Fields
        protected Elevator _elevator = default;
        protected Collider2D _collider2D = default;
        protected Rigidbody2D _rigidbody2D = default;
        protected Projectile2D _projectile2D = default;

        protected float _bobbingHeight = 0.0625f;
        protected float _bobbingSpeed = 8f;
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            _elevator = GetComponent<Elevator>();
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _projectile2D = GetComponent<Projectile2D>();
        }
        #endregion

        #region Public Methods
        public void Attach(Transform target, float height)
        {
            // attach to parent
            transform.SetParent(target);

            // play raise animation
            StopAllCoroutines();
            StartCoroutine(MoveTo(Vector3.zero));
            StartCoroutine(ElevateTo(height));

            // disable physics
            _projectile2D.ResetVelocity();
            _projectile2D.IsKinematic = true;
            _projectile2D.enabled = false;

            // disable collisions
            _collider2D.enabled = false;
            _rigidbody2D.isKinematic = true;
        }

        public void Detach()
        {
            // detach from parent
            transform.SetParent(null);

            // play lower animation
            StopAllCoroutines();
            StartCoroutine(ElevateTo(0f));

            // enable physics
            _projectile2D.IsKinematic = false;
            _projectile2D.enabled = true;

            // enable collisions
            _collider2D.enabled = true;
            _rigidbody2D.isKinematic = false;
        }

        public abstract void Use(Vector2 direction);
        #endregion

        #region Private Methods
        private IEnumerator ElevateTo(float targetHeight)
        {
            targetHeight = Mathf.Max(targetHeight, 0f);
            while(Mathf.Abs(_elevator.height - targetHeight) > 0.05f)
            {
                _elevator.height = Mathf.Lerp(_elevator.height, targetHeight, Time.deltaTime * 10f);
                yield return null;
            }
            _elevator.height = targetHeight;

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
                _elevator.height = Mathf.Sin(bobbingProgress - sinShift) * _bobbingHeight + _bobbingHeight;
                yield return null;
            }
        }
        #endregion
    }
}