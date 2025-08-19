using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Events;

namespace Game.UI.Overlay
{
    public class InventorySizeRequestExecutor : OverlayRequestExecutor<InventorySizeRequest>
    {
        #region fields & properties
        [SerializeField] private InventorySizeExposer sizeExposer;
        #endregion fields & properties

        #region methods
        protected override void ExecuteRequest(InventorySizeRequest req)
        {
            sizeExposer.Expose(req.Inventory, req.MaxSize);
        }
        #endregion methods
    }
}