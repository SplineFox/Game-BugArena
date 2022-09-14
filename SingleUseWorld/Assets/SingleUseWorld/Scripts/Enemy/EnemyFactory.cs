﻿using SingleUseWorld.FSM;
using System;
using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "EnemyFactorySO", menuName = "SingleUseWorld/Factories/Enemies/Enemy Factory SO")]
    public sealed class EnemyFactory : ScriptableFactory, IMonoFactory<Enemy>
    {
        #region Fields
        [SerializeField] private Enemy _enemyPrefab;
        #endregion

        #region Public Methods
        public Enemy Create()
        {
            var enemy = CreateInstance<Enemy>(_enemyPrefab);
            enemy.OnCreate();
            return enemy;
        }
        #endregion
    }
}