using Cinemachine;
using UnityEngine;

namespace BugArena
{
    public class ArenaCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraShaker _cameraShaker;
        [SerializeField] private CameraTargeter _cameraTargeter;

        public Camera Main { get => _camera; }
        public CameraShaker Shaker { get => _cameraShaker; }
        public CameraTargeter Targeter { get => _cameraTargeter; }
    }
}