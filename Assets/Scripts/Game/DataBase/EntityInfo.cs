using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class EntityInfo
    {
        #region fields & properties
        /// <summary>
        /// Returns clone of 'stats' to prevent db modifying
        /// </summary>
        public EntityStats Stats => stats.Clone();
        [SerializeField] private EntityStats stats = new();
        public Entity Prefab => prefab;
        [SerializeField] private Entity prefab;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}