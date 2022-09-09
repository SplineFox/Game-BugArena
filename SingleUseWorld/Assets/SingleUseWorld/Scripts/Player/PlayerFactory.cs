using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "PlayerFactorySO", menuName = "SingleUseWorld/Factories/Player/Player Factory SO")]
    public class PlayerFactory : MonoFactory<Player>
    {
        protected override void OnAfterCreate(Player instance)
        {
            instance.Initialize();
        }

        protected override void OnBeforeDestroy(Player instance)
        {
            instance.Deinitialize();
        }
    }
}
