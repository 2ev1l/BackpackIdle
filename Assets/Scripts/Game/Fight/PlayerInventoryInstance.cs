using DebugStuff;
using EditorCustom.Attributes;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class PlayerInventoryInstance : InventoryInstance
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            OnListUpdate(GameData.Data.PlayerData.Inventory);
        }
        #endregion methods

#if UNITY_EDITOR
        [Title("Tests")]
        [SerializeField][DontDraw] private bool ___testBool;
        [SerializeField][Range(0, 4)] private int itemId = 0;
        [SerializeField] private Vector2Int cellId = new(0, 0);
        [Button(nameof(AddItem))]
        private void AddItem()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            ItemData itemData = new(itemId, -1);
            GameData.Data.PlayerData.Inventory.TryPlaceItem(itemData, cellId.x, cellId.y);
        }
        [Button(nameof(RemoveItem))]
        private void RemoveItem()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            GameData.Data.PlayerData.Inventory.RemoveItem(itemId);
        }
#endif //UNITY_EDITOR

    }
}