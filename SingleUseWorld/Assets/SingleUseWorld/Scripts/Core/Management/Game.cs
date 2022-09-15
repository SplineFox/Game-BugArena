using Cinemachine;
using UnityEngine;

namespace SingleUseWorld
{
    public class Game : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Camera _camera = default;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;
        [Space]
        [SerializeField] private PlayerInput _playerInput = default;
        [SerializeField] private PlayerFactory _playerFactory = default;
        [Space]
        [SerializeField] private EnemyFactory _enemyFactory = default;
        [Space]
        [SerializeField] private SkullItemFactory _skullItemFactory = default;
        [SerializeField] private SkullEntityFactory _skullEntityFactory = default;

        private Player _player;
        private PlayerController _playerController;
        private CameraController _cameraController;
        private TargetController _targetController;
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Deinitialize();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void Initialize()
        {
            _player = _playerFactory.Create();
            _playerController = new PlayerController();
            _cameraController = new CameraController(_camera, _virtualCamera);
            _targetController = new TargetController(_player.transform, _cameraController);

            _playerController.Initialize(_playerInput, _player, _cameraController, _targetController);

            var enemy = _enemyFactory.Create();
            enemy.transform.position = _player.transform.position + Vector3.left * 5;

            var item = _skullItemFactory.Create();
            item.transform.position = _player.transform.position + Vector3.right * 5;
        }

        private void Deinitialize()
        {
            GameObject.Destroy(_player);
        }
        #endregion
    }
}