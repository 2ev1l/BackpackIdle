using Game.DataBase;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections.Generic;

namespace Game.UI.Overlay
{
    public class ItemEffectList : ContextInfinityList<EffectStatInfo>
    {
        #region fields & properties
        private readonly List<EffectStatInfo> effects = new();
        private ItemInfo info = null;
        private int level = 1;
        #endregion fields & properties

        #region methods
        public void Initialize(ItemInfo itemInfo, int level)
        {
            info = itemInfo;
            this.level = level;
            UpdateListData();
        }
        private void UpdateEffects()
        {
            effects.Clear();
            info.GetEffectStatInfos(level, effects);
        }
        public override void UpdateListData()
        {
            if (info == null) return;
            UpdateEffects();
            ItemList.UpdateListDefault(effects, x => x);
        }
        #endregion methods
    }
}