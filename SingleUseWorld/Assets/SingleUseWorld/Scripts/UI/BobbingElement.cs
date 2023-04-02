using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class BobbingElement : MonoBehaviour
    {
        [SerializeField] public float bobbingHeight = 0.2f;
        [SerializeField] public float rotationAngle = 4f;
        [SerializeField] public float speed = 4f;

        private float bobbingProgress = 0f;

        private void Start()
        {
        }

        private void Update()
        {
            var value = Mathf.Sin(bobbingProgress);
            transform.localPosition += Vector3.up * value * bobbingHeight;
            transform.eulerAngles = Vector3.forward * value * rotationAngle;

            bobbingProgress += speed * Time.deltaTime;
        }
    }
}