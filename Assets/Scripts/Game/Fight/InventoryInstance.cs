using EditorCustom.Attributes;
using Game.Serialization.World;
using Game.UI.Collections;
using Game.UI.Overlay;
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

        public RectTransform ItemsDefaultParent => itemsFactory.DefaultItemsParent;
        public RectTransform ItemsParent => itemsFactory.ItemsParent;
        [SerializeField] private RectTransform backpackTransform;
        [SerializeField] private ItemsFactory itemsFactory;
        [SerializeField] private Vector2Int sizeLimit = new(6, 6);
        [SerializeField] private RandomItemsGenerator randomItemsGenerator;
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
            int cellId = Context.FindTopLeftCellOfItem(itemData.DataId);
            itemsFactory.SpawnItemAtPosition(Context, itemData, cellId);
        }
        private void UpdateBackpackUI()
        {
            LoadItems();
            FixBackpackTransform();
        }
        public void LoadItems()
        {
            InitializeItems();
            itemsFactory.ClearItems();
            itemsFactory.SpawnItemsAtPositions(Context, itemsAtPositions);
            if (randomItemsGenerator.HasGeneratedItems(Context)) return;
            GenerateRandomItems();
        }

        [SerializedMethod]
        public void GenerateRandomItems()
        {
            randomItemsGenerator.GenerateRandomItems(Context);
        }
        [SerializedMethod]
        public void SendIncreaseSizeRequest()
        {
            new InventorySizeRequest(Context, sizeLimit).Send();
        }
        [SerializedMethod]
        public void IncreaseVerticalSize(int value)
        {
            int maxIncrease = sizeLimit.y - Context.Height;
            value = Mathf.Min(maxIncrease, value);
            if (value <= 0) return;
            Context.IncreaseHeight(value);
        }
        [SerializedMethod]
        public void IncreaseHorizontalSize(int value)
        {
            int maxIncrease = sizeLimit.x - Context.Width;
            value = Mathf.Min(maxIncrease, value);
            if (value <= 0) return;
            Context.IncreaseWidth(value);
        }
        public void FixBackpackTransform()
        {
            backpackTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GetGridWidth());
            backpackTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetGridHeight());
        }
        private float GetGridWidth()
        {
            int horizontalOversize = Context.Width - InventoryData.DEFAULT_SIZE;
            return DEFAULT_BACKPACK_SIZE + horizontalOversize * ITEM_CELL_SIZE;
        }
        private float GetGridHeight()
        {
            int verticalOversize = Context.Height - InventoryData.DEFAULT_SIZE;
            return DEFAULT_BACKPACK_SIZE + verticalOversize * ITEM_CELL_SIZE;
        }

        public int GetCellIndexFromPosition(Vector2 anchoredPosition)
        {
            float localX = anchoredPosition.x;
            float localY = -anchoredPosition.y;

            if (localX < 0 || localY < 0)
            {
                return -1;
            }

            float totalGridWidth = GetGridWidth();
            float totalGridHeight = GetGridHeight();

            if (localX >= totalGridWidth || localY >= totalGridHeight)
            {
                return -1;
            }

            int cellX = Mathf.FloorToInt(localX / ITEM_CELL_SIZE);
            int cellY = Mathf.FloorToInt(localY / ITEM_CELL_SIZE);

            if (cellX >= Context.Width || cellY >= Context.Height)
            {
                return -1;
            }

            int index = cellY * Context.Width + cellX;

            return index;

        }

        public override void OnListUpdate(InventoryData param)
        {
            base.OnListUpdate(param);
            UpdateBackpackUI();
        }
        #endregion methods
    }
}