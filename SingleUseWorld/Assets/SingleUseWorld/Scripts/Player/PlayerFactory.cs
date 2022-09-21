using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "PlayerFactorySO", menuName = "SingleUseWorld/Factories/Player/Player Factory SO")]
    public class PlayerFactory : ScriptableFactory, IMonoFactory<Player>
    {
        #region Fields
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private PlayerSettings _playerSettings;
        #endregion

        #region Public Methods
        public Player Create()
        {
            var player = CreateInstance<Player>(_playerPrefab);
            player.OnCreate(_playerSettings);
            return player;
        }
        #endregion
    }
}
