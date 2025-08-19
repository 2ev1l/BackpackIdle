using EditorCustom.Attributes;
using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    [System.Serializable]
    public class RandomItemsGenerator
    {
        #region fields & properties
        [SerializeField][MinMaxSlider(1, 4)] private Vector2Int randomItemsCount = new(1, 3);
        private readonly List<int> itemsToRemove = new();
        #endregion fields & properties

        #region methods
        public bool HasGeneratedItems(InventoryData inventory)
        {
            foreach (var item in inventory.ItemsData.Items)
            {
                if (inventory.FindTopLeftCellOfItem(item.DataId) != InventoryData.CLEAR) continue;
                return true;
            }
            return false;
        }
        private void RemoveOldGeneratedItems(InventoryData inventory)
        {
            itemsToRemove.Clear();
            foreach (var item in inventory.ItemsData.Items)
            {
                if (inventory.FindTopLeftCellOfItem(item.DataId) != InventoryData.CLEAR) continue;
                itemsToRemove.Add(item.DataId);
            }
            foreach (var el in itemsToRemove)
            {
                inventory.ItemsData.RemoveItem(el);
            }
        }
        public void GenerateRandomItems(InventoryData toInventory)
        {
            RemoveOldGeneratedItems(toInventory);
            int randomCount = Random.Range(randomItemsCount.x, randomItemsCount.y + 1);
            int totalItems = DB.Instance.ItemsInfo.Data.Count;
            for (int i = 0; i < randomCount; ++i)
            {
                int randomId = Random.Range(0, totalItems);
                toInventory.AddEmptyItem(randomId);
            }
        }
        #endregion methods
    }
}