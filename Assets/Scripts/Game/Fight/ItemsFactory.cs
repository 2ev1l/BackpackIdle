using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;

namespace Game.Fight
{
    [System.Serializable]
    public class ItemsFactory
    {
        #region fields & properties
        public RectTransform ItemsParent => (RectTransform)instancesPool.ParentForSpawn;
        public RectTransform DefaultItemsParent => defaultItemsParent;
        [SerializeField] private RectTransform defaultItemsParent;
        [SerializeField] private ObjectPool<StaticPoolableObject> instancesPool;
        #endregion fields & properties

        #region methods
        public void ClearItems()
        {
            instancesPool.DisableObjects();
        }
        public void SpawnItemsAtPositions(InventoryData inventory, Dictionary<ItemData, int> itemsAtPositions)
        {
            foreach (var kv in itemsAtPositions)
            {
                SpawnItemAtPosition(inventory, kv.Key, kv.Value);
            }
        }
        public ItemInstance SpawnItemAtPosition(InventoryData inventory, ItemData item, int itemPosition)
        {
            ItemInstance instance = SpawnItem(item);
            if (itemPosition == InventoryData.CLEAR)
            {
                MoveItemToDefaultSpawn(instance);
            }
            else
            {
                MoveItemToPosition(inventory, instance, itemPosition);
            }
            return instance;
        }
        private ItemInstance SpawnItem(ItemData item)
        {
            ItemInstance instance = (ItemInstance)instancesPool.GetObject();
            instance.Initialize(item);
            return instance;
        }
        public void RemoveItem(ItemData item)
        {
            ItemInstance instance = null;
            foreach (ItemInstance el in instancesPool.Objects.Cast<ItemInstance>())
            {
                if (el.Data != item) continue;
                instance = el;
            }
            if (instance == null) return;
            if (instance.IsSelected) return;
            instance.DisableObject();
        }
        private void MoveItemToDefaultSpawn(ItemInstance item)
        {
            item.transform.SetParent(defaultItemsParent);
        }
        private void MoveItemToPosition(InventoryData inventory, ItemInstance item, int position)
        {
            float defaultOffset = InventoryInstance.CELL_PADDING - InventoryInstance.CELL_SPACING / 2;
            RectTransform rt = ((RectTransform)item.transform);
            Vector2 finalPosition = new(rt.rect.width + defaultOffset, -defaultOffset);
            Vector2Int offsetScale = inventory.GetCoordinatesFromIndex(position);
            finalPosition.x += offsetScale.x * InventoryInstance.ITEM_CELL_SIZE;
            finalPosition.y -= offsetScale.y * InventoryInstance.ITEM_CELL_SIZE;
            rt.anchoredPosition = finalPosition;
        }
        #endregion methods
    }
}