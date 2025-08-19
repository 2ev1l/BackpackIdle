using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;

namespace Game.Fight
{
    [System.Serializable]
    public class EnemyFactory
    {
        #region fields & properties
        [SerializeField] private ObjectPool<StaticPoolableObject> enemyPool;
        [SerializeField] private Transform positionForSpawn;
        #endregion fields & properties

        #region methods
        public void RemoveEnemies()
        {
            enemyPool.DisableObjects();
        }
        public void SpawnEnemy(EnemyInfo info)
        {
            EnemyInstance enemy = enemyPool.GetObject() as EnemyInstance;
            enemy.transform.localPosition = positionForSpawn.localPosition;
            enemy.Initialize(info);
        }
        #endregion methods
    }
}