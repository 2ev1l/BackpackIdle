using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Universal.Core;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class InventoryData
    {
        #region fields & properties
        public const int CLEAR = -1;
        public const int DEFAULT_SIZE = 3;
        public UnityAction OnSizeChanged;
        public ItemsData ItemsData => itemsData;
        [SerializeField] private ItemsData itemsData = new();
        [SerializeField] private List<int> items = new() { CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR, CLEAR };
        public int Width => width;
        [SerializeField][Min(1)] private int width = DEFAULT_SIZE;
        public int Height => height;
        [SerializeField][Min(1)] private int height = DEFAULT_SIZE;
        #endregion fields & properties

        #region methods
        public Vector2Int GetCoordinatesFromIndex(int index)
        {
            int x = index % width;
            int y = index / width;
            return new Vector2Int(Mathf.Abs(x), y);
        }

        public int FindTopLeftCellOfItem(int itemIdToFind)
        {
            if (itemIdToFind == CLEAR)
            {
                return -1;
            }
            return items.IndexOf(itemIdToFind);
        }
        public void IncreaseHeight() => IncreaseHeight(1);
        public void IncreaseHeight(int amountToAdd)
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
            OnSizeChanged?.Invoke();
        }

        public void IncreaseWidth() => IncreaseWidth(1);
        public void IncreaseWidth(int amountToAdd)
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
            OnSizeChanged?.Invoke();
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

        public void AddEmptyItem(int infoId, int level = 1)
        {
            itemsData.AddItem(infoId, level);
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
                return wasUpgraded;
            }

            Shape itemShape = itemToPlace.Info.ItemInfo.Shape;
            int itemID = ItemsData.GetFreeId();

            for (int y = 0; y < itemShape.Height; y++)
            {
                for (int x = 0; x < itemShape.Width; x++)
                {
                    if (!itemShape.GetValueAt(x, y)) continue;
                    SetItemAt(itemID, startX + x, startY + y);
                }
            }
            ItemsData.AddItem(itemToPlace.Id, itemToPlace.Level);
            return true;
        }
        public bool TryUpgradeItem(ItemData movedItem, ItemData occupiedItem)
        {
            bool canUpgrade = occupiedItem.CanUpgrade() &&
                              movedItem.Info.Id == occupiedItem.Info.Id &&
                              movedItem.Level == occupiedItem.Level;

            if (!canUpgrade) return false;
            bool wasUpgraded = occupiedItem.TryUpgrade();
            if (wasUpgraded)
                RemoveItem(movedItem.DataId);
            return wasUpgraded;
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