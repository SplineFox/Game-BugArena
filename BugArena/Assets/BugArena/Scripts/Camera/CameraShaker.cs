using Cinemachine;
using System.Collections;
using UnityEngine;

namespace BugArena
{
    public class CameraShaker : MonoBehaviour
    {
        #region Fields
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;
        #endregion

        #region Properties
        public bool IsShaking
        {
            get => _cinemachinePerlin.m_AmplitudeGain != 0f;
        }
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachinePerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        #endregion

        #region Public Methods
        public void Shake(float intensity, float duration = 0f)
        {
            _cinemachinePerlin.m_AmplitudeGain = intensity;
            
            if (duration != 0f)
                StartCoroutine(ShakeCoroutine(duration));
        }

        public void StopShake()
        {
            _cinemachinePerlin.m_AmplitudeGain = 0f;
            StopAllCoroutines();
        }
        #endregion

        #region Private Methods
        private IEnumerator ShakeCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            _cinemachinePerlin.m_AmplitudeGain = 0f;
        }
        #endregion
    }
}