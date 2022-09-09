using SingleUseWorld.FSM;
using System;
using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "EnemyFactorySO", menuName = "SingleUseWorld/Factories/Enemies/Enemy Factory SO")]
    public sealed class EnemyFactory : MonoFactory<Enemy>
    {
        #region Public Methods
        protected override void OnAfterCreate(Enemy instance)
        {
            instance.Initialize();
        }

        protected override void OnBeforeDestroy(Enemy instance)
        {
            instance.Deinitialize();
        }
        #endregion
    }
}