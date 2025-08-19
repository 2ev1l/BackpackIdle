using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;
using Universal.Events;

namespace Game.UI.Overlay
{
    public class ItemInfoRequestExecutor : OverlayRequestExecutor<ItemInfoRequest>
    {
        #region fields & properties
        [SerializeField] private ItemInfoExposer infoExposer;
        #endregion fields & properties

        #region methods
        protected override void ExecuteRequest(ItemInfoRequest req)
        {
            infoExposer.Expose(req.ItemInfo, req.Level);
        }
        #endregion methods
    }
}