using DebugStuff;
using EditorCustom.Attributes;
using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal.Collections;
using Universal.Core;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR


namespace Game.Fight
{
    public class ItemInstance : StaticPoolableObject
    {
        #region fields & properties
        [SerializeField] private Image icon;
        #endregion fields & properties

        #region methods

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
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,InventoryInstance.ITEM_CELL_SIZE * shape.Width);
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