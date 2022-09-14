using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "PlayerFactorySO", menuName = "SingleUseWorld/Factories/Player/Player Factory SO")]
    public class PlayerFactory : ScriptableFactory, IMonoFactory<Player>
    {
        #region Fields
        [SerializeField] private Player _playerPrefab;
        #endregion

        #region Public Methods
        public Player Create()
        {
            var player = CreateInstance<Player>(_playerPrefab);
            player.OnCreate();
            return player;
        }
        #endregion
    }
}
