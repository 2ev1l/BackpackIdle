using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class ItemData : DBData<ItemInfoDB>
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - New Level
        /// </summary>
        public UnityAction<int> OnUpgraded;
        public int DataId => dataId;
        [SerializeField][Min(0)] private int dataId = 0;
        public int Level => level;
        [SerializeField][Min(1)] private int level = 1;
        #endregion fields & properties

        #region methods
        protected override ItemInfoDB GetInfo() => DB.Instance.ItemsInfo.Data[Id].Data;
        public bool CanUpgrade() => Info.ItemInfo.CanUpgrade(level);
        public bool TryUpgrade()
        {
            if (!CanUpgrade()) return false;
            level++;
            OnUpgraded?.Invoke(level);
            return true;
        }

        public ItemData(int id, int dataId, int level = 1) : base(id)
        {
            this.dataId = dataId;
            this.level = level;
        }
        #endregion methods
    }
}