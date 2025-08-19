using Game.Serialization.World;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class InventoryCellList : ContextInfinityList<int>
    {
        #region fields & properties
        private readonly List<int> cellsId = new();
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            GameData.Data.PlayerData.Inventory.OnSizeChanged += UpdateListData;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            GameData.Data.PlayerData.Inventory.OnSizeChanged -= UpdateListData;
        }
        private void UpdateCellsId()
        {
            cellsId.Clear();
            int totalCells = GameData.Data.PlayerData.Inventory.Height * GameData.Data.PlayerData.Inventory.Width;
            for (int i = 0; i < totalCells; ++i)
            {
                cellsId.Add(i);
            }
        }
        public override void UpdateListData()
        {
            UpdateCellsId();
            ItemList.UpdateListDefault(cellsId, x => x);
        }
        #endregion methods
    }
}