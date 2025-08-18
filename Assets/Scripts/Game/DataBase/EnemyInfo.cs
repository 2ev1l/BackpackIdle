using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class EnemyInfo : EntityInfo
    {
        #region fields & properties
        public Entity Prefab => prefab;
        [SerializeField] private Entity prefab;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}