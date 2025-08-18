using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    [System.Serializable]
    public class ItemsFactory
    {
        #region fields & properties
        public RectTransform ItemsParent => itemsParent;
        [SerializeField] private RectTransform itemsParent;
        /// <summary>
        /// Can be made with <see cref="Universal.Collections.Generic.ObjectPool{T}"/> for garbage optimization
        /// but it requires to remove <see cref="WeaponInstance"/> 
        /// and use only <see cref="ItemInstance"/>
        /// </summary>
        private readonly Dictionary<ItemData, ItemInstance> instances = new();
        #endregion fields & properties

        #region methods
        public void MoveItemsToPositions(InventoryData inventory, Dictionary<ItemData, int> itemsAtPositions)
        {
            foreach (var kv in itemsAtPositions)
            {
                MoveItemToPosition(inventory, instances[kv.Key], kv.Value);
            }
        }
        private void ClearInstances()
        {
            foreach (var kv in instances)
            {
                GameObject.Destroy(kv.Value.gameObject);
            }
            instances.Clear();
        }
        public void SpawnItemsAtPositions(InventoryData inventory, Dictionary<ItemData, int> itemsAtPositions)
        {
            ClearInstances();
            foreach (var kv in itemsAtPositions)
            {
                SpawnItemAtPosition(inventory, kv.Key, kv.Value);
            }
        }
        public ItemInstance SpawnItemAtPosition(InventoryData inventory, ItemData item, int itemPosition)
        {
            ItemInstance instance = SpawnItem(item);
            MoveItemToPosition(inventory, instance, itemPosition);
            return instance;
        }
        private ItemInstance SpawnItem(ItemData item)
        {
            ItemInstance instance = GameObject.Instantiate<ItemInstance>(item.Info.ItemInfo.Prefab as ItemInstance, itemsParent);
            instance.Initialize(item);
            instances.Add(item, instance);
            return instance;
        }
        public void RemoveItem(ItemData item)
        {
            ItemInstance instance = instances[item];
            instances.Remove(item);
            if (instance.IsSelected) return;
            GameObject.Destroy(instance.gameObject);
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