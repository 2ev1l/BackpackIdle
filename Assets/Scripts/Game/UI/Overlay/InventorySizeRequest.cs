using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Events;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class InventorySizeRequest : ExecutableRequest
    {
        #region fields & properties
        public Vector2Int MaxSize => maxSize;
        private Vector2Int maxSize;
        public InventoryData Inventory => inventory;
        private InventoryData inventory;
        #endregion fields & properties

        #region methods
        public override void Close() { }
        public InventorySizeRequest(InventoryData inventory, Vector2Int maxSize)
        {
            this.inventory = inventory;
            this.maxSize = maxSize;
        }
        #endregion methods

    }
}