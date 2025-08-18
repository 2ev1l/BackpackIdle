using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class EnemyInfoDB : DBInfo
    {
        #region fields & properties
        public EnemyInfo Info => info;
        [SerializeField] private EnemyInfo info;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}