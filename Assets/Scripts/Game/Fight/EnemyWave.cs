using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class EnemyWave : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private EnemyFactory enemyFactory;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            StartWave();
        }
        private void OnDisable()
        {
            enemyFactory.RemoveEnemies();
        }
        private void StartWave()
        {
            int enemyId = 0;
            enemyFactory.SpawnEnemy(DB.Instance.EnemiesInfo[enemyId].Data.Info);
        }
        #endregion methods
    }
}