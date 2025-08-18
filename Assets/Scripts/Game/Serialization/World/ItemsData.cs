using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DataBase
{
    [System.Serializable]
    public class ItemsData
    {
        #region fields & properties
        public UnityAction<ItemData> OnItemAdded;
        public UnityAction<ItemData> OnItemRemoved;

        public IReadOnlyList<ItemData> Items => items;
        [SerializeField] private List<ItemData> items = new();
        [SerializeField][Min(0)] private int itemCounter = 0;
        #endregion fields & properties

        #region methods
        public void RemoveItem(int dataId)
        {
            ItemData item = FindItem(dataId);
            if (item == null) return;
            items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }
        public ItemData AddItem(int infoId)
        {
            if (infoId < 0) return null;
            ItemData item = new(infoId, itemCounter);
            itemCounter++;
            items.Add(item);
            OnItemAdded?.Invoke(item);
            return item;
        }
        public ItemData FindItem(int dataId) => items.Find(x => x.DataId == dataId);
        #endregion methods
    }
}