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

        #region LifeCycle Methods
        public void Awake()
        {
            _cinemachinePerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        #endregion

        #region Public Methods
        public void Shake(float intensity, float duration)
        {
            _cinemachinePerlin.m_AmplitudeGain = intensity;
            StartCoroutine(Wait(duration));

        }
        #endregion
	
    	#region Internal Methods
        #endregion

        #region Protected Methods
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