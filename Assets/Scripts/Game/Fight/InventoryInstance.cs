using EditorCustom.Attributes;
using Game.Serialization.World;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections.Generic;
using Universal.Core;

namespace Game.Fight
{
    public class InventoryInstance : ContextActionsItem<InventoryData>
    {
        #region fields & properties
        public const int ITEM_CELL_SIZE = BASE_CELL_SIZE + CELL_SPACING;
        public const int BASE_CELL_SIZE = 256;
        public const int CELL_SPACING = 32;
        public const int CELL_PADDING = 96;
        public const int DEFAULT_BACKPACK_SIZE = 1024;

        [SerializeField] private RectTransform backpackTransform;
        [SerializeField] private ItemsFactory itemsFactory;
        [SerializeField] private Vector2Int sizeLimit = new(6, 6);
        private readonly Dictionary<ItemData, int> itemsAtPositions = new();
        #endregion fields & properties

        #region methods
        protected override void OnSubscribe()
        {
            base.OnSubscribe();
            Context.OnSizeChanged += FixBackpackTransform;
            Context.ItemsData.OnItemAdded += LoadItem;
            Context.ItemsData.OnItemRemoved += RemoveItem;
        }
        protected override void OnUnSubscribe()
        {
            base.OnUnSubscribe();
            Context.OnSizeChanged -= FixBackpackTransform;
            Context.ItemsData.OnItemAdded -= LoadItem;
            Context.ItemsData.OnItemRemoved -= RemoveItem;
        }
        private void InitializeItems()
        {
            itemsAtPositions.Clear();
            InventoryData inventory = Context;
            foreach (ItemData item in inventory.ItemsData.Items)
            {
                int itemCellId = inventory.FindTopLeftCellOfItem(item.DataId);
                itemsAtPositions.Add(item, itemCellId);
            }
        }

        public void RemoveItem(ItemData itemData)
        {
            itemsFactory.RemoveItem(itemData);
        }
        public void LoadItem(ItemData itemData)
        {
            itemsFactory.SpawnItemAtPosition(Context, itemData, Context.FindTopLeftCellOfItem(itemData.DataId));
        }
        private void UpdateBackpackUI()
        {
            LoadItems();
            FixBackpackTransform();
        }
        public void LoadItems()
        {
            InitializeItems();
            itemsFactory.SpawnItemsAtPositions(Context, itemsAtPositions);
        }
        [SerializedMethod]
        public void IncreaseVerticalSize()
        {
            if (Context.Height >= sizeLimit.y) return;
            Context.IncreaseHeight();
        }
        [SerializedMethod]
        public void IncreaseHorizontalSize()
        {
            if (Context.Width >= sizeLimit.x) return;
            Context.IncreaseWidth();
        }
        public void FixBackpackTransform()
        {
            int verticalOversize = Context.Height - InventoryData.DEFAULT_SIZE;
            int horizontalOversize = Context.Width - InventoryData.DEFAULT_SIZE;
            backpackTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, DEFAULT_BACKPACK_SIZE + horizontalOversize * ITEM_CELL_SIZE);
            backpackTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DEFAULT_BACKPACK_SIZE + verticalOversize * ITEM_CELL_SIZE);
        }
        public override void OnListUpdate(InventoryData param)
        {
            base.OnListUpdate(param);
            UpdateBackpackUI();
        }
        #endregion methods
    }
}