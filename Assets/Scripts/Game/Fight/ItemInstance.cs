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
            icon.sprite = data.Info.ItemInfo.GetLevelInfo(data.Level).ItemIcon;
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
                Destroy(gameObject);
                return;
            }
            if (storedIndex > 0 && TryPlaceItemByIndex(storedIndex))
            {
                Destroy(gameObject);
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
            Debug.Log("Info");
        }
        #endregion methods

#if UNITY_EDITOR
        [Title("Generate prefab params")]
        [SerializeField] private ItemInfoDBSO itemInfo;
        [SerializeField][Min(1)] private int levelToSet = 1;
        [Button(nameof(GeneratePrefabFromInfo))]
        protected virtual void GeneratePrefabFromInfo()
        {
            SetItemParams();
            if (FixPrefabInstance()) return;
            CreatePrefab();
        }

        protected bool FixPrefabInstance()
        {
            Undo.RegisterCompleteObjectUndo(gameObject, "Switch components");
            ItemInstance newInstance = null;
            bool GetNewInstanceOrReturn<T>() where T : ItemInstance
            {
                if (GetType().Equals(typeof(T))) return false;
                newInstance = gameObject.AddComponent<T>();
                return true;
            }
            switch (itemInfo.Data.ItemInfo)
            {
                case WeaponInfo:
                    if (!GetNewInstanceOrReturn<WeaponInstance>()) return false;
                    break;
                default:
                    if (!GetNewInstanceOrReturn<ItemInstance>()) return false;
                    break;
            }

            newInstance.icon = this.icon;
            newInstance.itemInfo = this.itemInfo;
            newInstance.levelToSet = this.levelToSet;

            DestroyImmediate(this, true);
            newInstance.CreatePrefab();
            EditorUtility.SetDirty(newInstance.gameObject);
            return true;
        }
        protected void SetItemParams()
        {
            Undo.RegisterCompleteObjectUndo(icon, "Change texture");
            ItemInfo info = itemInfo.Data.ItemInfo;
            try { icon.sprite = info.GetLevelInfo(levelToSet).ItemIcon; }
            catch { Debug.Log("Level is not defined"); }
            EditorUtility.SetDirty(icon);

            Undo.RegisterCompleteObjectUndo(gameObject, "Change rect transform size");
            Shape shape = info.Shape;
            RectTransform rt = GetComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, InventoryInstance.ITEM_CELL_SIZE * shape.Width);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InventoryInstance.ITEM_CELL_SIZE * shape.Height);
            EditorUtility.SetDirty(gameObject);
        }
        protected void CreatePrefab()
        {
            string newName = $"Item Instance {itemInfo.Id}";
            string assetPath = $"Assets/Prefabs/DB/Items/{newName}.prefab";
            Undo.RegisterCompleteObjectUndo(gameObject, "Set prefab as object");
            PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);
            GameObject obj = PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, assetPath, InteractionMode.UserAction);
            EditorUtility.SetDirty(gameObject);

            StaticPoolableObject spo = obj.GetComponent<StaticPoolableObject>();
            Undo.RegisterCompleteObjectUndo(itemInfo.Data.ItemInfo, "Set item info prefab");
            itemInfo.Data.ItemInfo.Prefab = spo;
            EditorUtility.SetDirty(itemInfo.Data.ItemInfo);
        }
#endif //UNITY_EDITOR

    }
}