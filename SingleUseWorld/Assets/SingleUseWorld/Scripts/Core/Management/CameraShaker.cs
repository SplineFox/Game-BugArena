using Cinemachine;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class CameraShaker : MonoBehaviour
    {
        #region Fields
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = default;
        private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;
        #endregion

        #region Properties
        public bool IsShaking
        {
            get => _cinemachinePerlin.m_AmplitudeGain != 0f;
        }
        #endregion

        #region LifeCycle Methods
        public void Awake()
        {
            _cinemachinePerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        #endregion

        #region Public Methods
        public void Shake(float intensity, float duration = 0f)
        {
            _cinemachinePerlin.m_AmplitudeGain = intensity;
            
            if (duration != 0f)
                StartCoroutine(Wait(duration));
        }

        public void StopShake()
        {
            _cinemachinePerlin.m_AmplitudeGain = 0f;
            StopAllCoroutines();
        }
        #endregion

        #region Private Methods
        private IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
            _cinemachinePerlin.m_AmplitudeGain = 0f;
        }
        #endregion
    }
}