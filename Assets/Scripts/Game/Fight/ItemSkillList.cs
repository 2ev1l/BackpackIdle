using Game.DataBase;
using Game.Serialization.World;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Fight
{
    public class ItemSkillList : ContextInfinityList<SkillDataPackage>
    {
        #region fields & properties
        private readonly List<SkillDataPackage> orderedItems = new();
        private readonly List<SkillDataPackage> items = new();
        #endregion fields & properties

        #region methods
        public void Initialize(InventoryData inventory, ISkillTargetProvider targetProvider)
        {
            items.Clear();
            orderedItems.Clear();
            foreach (var el in inventory.ItemsData.Items)
            {
                if (inventory.FindTopLeftCellOfItem(el.DataId) == InventoryData.CLEAR) continue;
                items.Add(new(el, targetProvider));
            }
            foreach (var el in items.OrderBy(x => x.ItemData.Info.ItemInfo is WeaponInfo ? 0 : 1))
            {
                orderedItems.Add(el);
            }
            UpdateListData();
        }
        public override void UpdateListData()
        {
            ItemList.UpdateListDefault(orderedItems, x => x);
        }
        #endregion methods
    }
}