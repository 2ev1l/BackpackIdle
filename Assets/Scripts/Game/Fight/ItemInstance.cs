using DebugStuff;
using EditorCustom.Attributes;
using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal.Collections;
using Universal.Core;
using Game.Serialization.World;
using Game.UI.Elements;
using UnityEngine.EventSystems;
using Universal.Events;
using Game.UI.Overlay;



#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR


namespace Game.Fight
{
    [RequireComponent(typeof(ItemMove))]
    [RequireComponent(typeof(CustomButton))]
    public class ItemInstance : StaticPoolableObject
    {
        #region fields & properties
        [SerializeField] private Image icon;
        private ItemMove ItemMove
        {
            get
            {
                if (itemMove == null)
                    itemMove = GetComponent<ItemMove>();
                return itemMove;
            }
        }
        private ItemMove itemMove;
        private CustomButton Button
        {
            get
            {
                if (button == null)
                    button = GetComponent<CustomButton>();
                return button;
            }
        }
        private CustomButton button;
        public ItemData Data => data;
        private ItemData data = null;
        public bool IsSelected => isSelected;
        private bool isSelected = false;
        private int storedIndex = -1;
        #endregion fields & properties

        #region methods
        public void Initialize(ItemData data)
        {
            UnSubscribe();
            this.data = data;
            UpdateUI();
            Subscribe();
        }
        private void OnEnable()
        {

        }
        private void OnDisable()
        {
            DeSelect();
        }
        private void Subscribe()
        {
            Button.OnClickEvent.AddListener(ShowInfo);
            ItemMove.OnMoveStart += OnMoveStart;
            ItemMove.OnMoveEnd += OnMoveEnd;
            if (data != null)
                data.OnUpgraded += UpdateUI;
        }
        private void UnSubscribe()
        {
            Button.OnClickEvent.RemoveListener(ShowInfo);
            ItemMove.OnMoveStart -= OnMoveStart;
            ItemMove.OnMoveEnd -= OnMoveEnd;
            if (data != null)
                data.OnUpgraded -= UpdateUI;
        }
        public void UpdateUI(int _) => UpdateUI();
        public void UpdateUI()
        {
            if (icon != null)
                icon.sprite = data.Info.ItemInfo.GetLevelInfo(data.Level).ItemIcon;
            FixObjectSize();
        }
        private void Select() => isSelected = true;
        private void DeSelect() => isSelected = false;
        private void OnMoveStart()
        {
            Select();
            TryRemoveItem();
        }
        private void OnMoveEnd()
        {
            TryChangePosition();
            DeSelect();
        }
        private void TryRemoveItem()
        {
            storedIndex = GameData.Data.PlayerData.Inventory.FindTopLeftCellOfItem(data.DataId);
            GameData.Data.PlayerData.Inventory.RemoveItem(data.DataId);
        }
        private void TryChangePosition()
        {
            RectTransform rt = (RectTransform)transform;
            Vector2 anchoredPosition = rt.anchoredPosition;
            anchoredPosition.x -= rt.rect.width;
            int cellId = PlayerInventoryInstance.Instance.GetCellIndexFromPosition(anchoredPosition);
            if (cellId == InventoryData.CLEAR)
            {
                ItemMove.ResetPositionToDefaultParent();
                return;
            }

            if (TryPlaceItemByIndex(cellId))
            {
                DisableObject();
                return;
            }
            if (storedIndex > 0 && TryPlaceItemByIndex(storedIndex))
            {
                DisableObject();
                return;
            }

            ItemMove.ResetPositionToDefaultParent();
        }
        private bool TryPlaceItemByIndex(int index)
        {
            Vector2Int coordinates = GameData.Data.PlayerData.Inventory.GetCoordinatesFromIndex(index);
            return GameData.Data.PlayerData.Inventory.TryPlaceItem(data, coordinates.x, coordinates.y);
        }
        public void ShowInfo()
        {
            if (isSelected) return;
            new ItemInfoRequest(data.Info.ItemInfo, data.Level).Send();
        }
        public void FixObjectSize() => FixObjectSize(data.Info.ItemInfo.Shape);
        private void FixObjectSize(Shape shape)
        {
            RectTransform rt = (RectTransform)transform;
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, InventoryInstance.ITEM_CELL_SIZE * shape.Width);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InventoryInstance.ITEM_CELL_SIZE * shape.Height);
        }
        #endregion methods
    }
}