using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Events;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class ItemInfoRequest : ExecutableRequest
    {
        #region fields & properties
        public ItemInfo ItemInfo => itemInfo;
        private ItemInfo itemInfo = null;
        public int Level => level;
        private int level = 1;
        #endregion fields & properties

        #region methods
        public override void Close() { }
        public ItemInfoRequest(ItemInfo itemInfo, int level)
        {
            this.itemInfo = itemInfo;
            this.level = level;
        }
        #endregion methods
    }
}