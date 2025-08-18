using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class PlayerData
    {
        #region fields & properties
        public InventoryData Inventory => inventory;
        [SerializeField] private InventoryData inventory = new();
        public EntityStats Stats => stats;
        [SerializeField] private EntityStats stats = new();
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}