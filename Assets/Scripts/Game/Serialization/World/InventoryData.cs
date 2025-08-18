using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Universal.Core;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class InventoryData
    {
        #region fields & properties
        public const int CLEAR = -1;
        public ItemsData ItemsData => itemsData;
        [SerializeField] private ItemsData itemsData = new();
        [SerializeField] private List<int> items = new() { CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR };
        public int Width => width;
        [SerializeField][Min(1)] private int width = 3;
        public int Height => height;
        [SerializeField][Min(1)] private int height = 3;
        #endregion fields & properties

        #region methods

        public int FindTopLeftCellOfItem(int itemIdToFind)
        {
            if (itemIdToFind == CLEAR)
            {
                return -1;
            }
            return items.IndexOf(itemIdToFind);
        }
        public void IncreaseHeight() => IncreaseHeight(1);
        private void IncreaseHeight(int amountToAdd)
        {
            if (amountToAdd <= 0)
            {
                Debug.LogError("Amount to add must be positive.");
                return;
            }

            int newHeight = this.height + amountToAdd;
            int cellsToAdd = amountToAdd * this.width;

            this.items.AddRange(Enumerable.Repeat<int>(CLEAR, cellsToAdd));

            this.height = newHeight;
        }

        public void IncreaseWidth() => IncreaseWidth(1);
        private void IncreaseWidth(int amountToAdd)
        {
            if (amountToAdd <= 0)
            {
                Debug.LogError("Amount to add must be positive");
                return;
            }

            int oldWidth = this.width;
            int newWidth = this.width + amountToAdd;

            var newItems = new List<int>(newWidth * this.height);

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < oldWidth; x++)
                {
                    newItems.Add(GetItemAt(x, y));
                }
                for (int i = 0; i < amountToAdd; i++)
                {
                    newItems.Add(CLEAR);
                }
            }

            this.items = newItems;
            this.width = newWidth;
        }
        public bool CanPlaceItem(ItemData itemToPlace, int startX, int startY, out ItemData occupiedItem)
        {
            occupiedItem = null;
            Shape itemShape = itemToPlace.Info.ItemInfo.Shape;
            if (startX < 0 || startY < 0 ||
                startX + itemShape.Width > this.width ||
                startY + itemShape.Height > this.height)
            {
                return false;
            }

            for (int y = 0; y < itemShape.Height; y++)
            {
                for (int x = 0; x < itemShape.Width; x++)
                {
                    if (!itemShape.GetValueAt(x, y)) continue;
                    int dataId = GetItemAt(startX + x, startY + y);
                    if (dataId != CLEAR)
                    {
                        occupiedItem = ItemsData.FindItem(dataId);
                        return false;
                    }
                }
            }

            return true;
        }

        public bool TryPlaceItem(ItemData itemToPlace, int startX, int startY)
        {
            if (!CanPlaceItem(itemToPlace, startX, startY, out ItemData occupiedItem))
            {
                if (occupiedItem == null)
                {
                    return false;
                }

                bool wasUpgraded = TryUpgradeItem(itemToPlace, occupiedItem);
                if (wasUpgraded)
                {
                    RemoveItem(itemToPlace.DataId);
                }

                return wasUpgraded;
            }

            Shape itemShape = itemToPlace.Info.ItemInfo.Shape;
            int itemID = itemToPlace.DataId;

            for (int y = 0; y < itemShape.Height; y++)
            {
                for (int x = 0; x < itemShape.Width; x++)
                {
                    if (!itemShape.GetValueAt(x, y)) continue;
                    SetItemAt(itemID, startX + x, startY + y);
                }
            }
            ItemsData.AddItem(itemToPlace.Id);
            return true;
        }
        private bool TryUpgradeItem(ItemData movedItem, ItemData occupiedItem)
        {
            bool canUpgrade = occupiedItem.CanUpgrade() &&
                              movedItem.Info.Id == occupiedItem.Info.Id &&
                              movedItem.Level == occupiedItem.Level;

            if (!canUpgrade) return false;

            return occupiedItem.TryUpgrade();
        }

        public void RemoveItem(int itemToRemove)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == itemToRemove)
                {
                    items[i] = CLEAR;
                }
            }
            itemsData.RemoveItem(itemToRemove);
        }

        private int GetItemAt(int x, int y)
        {
            int index = y * width + x;
            if (index < 0 || index >= items.Count)
            {
                Debug.LogError($"GetItemAt: {index} is out of bounds");
                return CLEAR;
            }
            return items[index];
        }
        private void SetItemAt(int itemId, int x, int y)
        {
            int index = y * width + x;
            if (index >= 0 && index < items.Count)
            {
                items[index] = itemId;
            }
            else
            {
                Debug.LogError($"SetItemAt: Invalid index {index}");
            }
        }

        #endregion methods
    }
}