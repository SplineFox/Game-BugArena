using Cinemachine;
using UnityEngine;

namespace SingleUseWorld
{
    public class TestInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput = default;
        [SerializeField] private Camera _camera = default;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;

        private Player _player = default;
        private PlayerController _playerController = default;
        private CameraController _cameraController = default;
        private TargetController _targetController = default;

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.Initialize();

            _playerController = new PlayerController();
            _cameraController = new CameraController(_camera, _virtualCamera);
            _targetController = new TargetController(_player.transform, _cameraController);

            _playerController.Initialize(_playerInput, _player, _cameraController, _targetController);
        }
    }
}