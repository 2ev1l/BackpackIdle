using UnityEngine;
using EditorCustom.Attributes;
using Universal.Core;
using System.Linq;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

namespace Game.DataBase
{
    [ExecuteAlways]
    public class DB : MonoBehaviour
    {
        #region fields & properties
        public static DB Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) //only in editor
                    return GameObject.FindFirstObjectByType<DB>(FindObjectsInactive.Include);
#endif //UNITY_EDITOR
                return instance;
            }
            set
            {
                if (instance == null)
                    instance = value;
            }
        }
        private static DB instance;

        public DBSOSet<ItemInfoDBSO> ItemsInfo => itemsInfo;
        [SerializeField] private DBSOSet<ItemInfoDBSO> itemsInfo;
        public DBSOSet<EnemyInfoDBSO> EnemiesInfo => enemiesInfo;
        [SerializeField] private DBSOSet<EnemyInfoDBSO> enemiesInfo;
        #endregion fields & properties

        #region methods

        #endregion methods

#if UNITY_EDITOR
        [SerializeField] private bool automaticallyUpdateEditor = false;
        private void OnValidate()
        {
            if (!automaticallyUpdateEditor) return;
            GetAllDBInfo();
            CheckAllErrors();
        }
        /// <summary>
        /// You need to manually add code
        /// </summary>
        [Button(nameof(GetAllDBInfo))]
        private void GetAllDBInfo()
        {
            if (Application.isPlaying) return;
            AssetDatabase.Refresh();
            Undo.RegisterCompleteObjectUndo(this, "Update DB");

            //call dbset.CollectAll()
            itemsInfo.CollectAll();
            enemiesInfo.CollectAll();

            EditorUtility.SetDirty(this);
        }
        /// <summary>
        /// You need to manually add code
        /// </summary>
        [Button(nameof(CheckAllErrors))]
        private void CheckAllErrors()
        {
            if (!Application.isPlaying) return;
            //call dbset.CatchExceptions(x => ...)
            System.Exception e = new();
            CatchItemsInfoExceptions(itemsInfo);

            enemiesInfo.CatchExceptions(x => x.Data.Info.Prefab == null, e, "Prefab must be not null");
            enemiesInfo.CatchExceptions(x => x.Data.Info.Stats.Health.Value <= 0, e, "Health must be > 0");
            enemiesInfo.CatchExceptions(x => x.Data.Info.Stats.MaxHealth.Value <= 0, e, "Max Health must be > 0");
        }
        private void CatchWeaponsInfoExceptions(DBSOSet<ItemInfoDBSO> itemsInfo, System.Func<WeaponInfo, bool> exceptionMatch, string errorMessage)
        {
            System.Exception e = new();
            itemsInfo.CatchExceptions(x =>
            {
                if (x.Data.ItemInfo is not WeaponInfo weaponInfo) return false;
                return exceptionMatch.Invoke(weaponInfo);
            }, e, errorMessage);
        }

        private void CatchItemsInfoExceptions(DBSOSet<ItemInfoDBSO> itemsInfo)
        {
            System.Exception e = new();
            itemsInfo.CatchExceptions(x => _ = x.Data.ItemInfo.Name, "Invalid name");
            itemsInfo.CatchExceptions(x => x.Data.ItemInfo.EffectBinding.Effect == null, e, "Effect in binding must be not null");
            itemsInfo.CatchExceptions(x => x.Data.ItemInfo.LevelsInfo.Count < 1, e, "Levels must be >= 1");
            itemsInfo.CatchExceptions(x => x.Data.ItemInfo.ActivationDelayInfo == null, e, "Activation delay must be not null");
            itemsInfo.CatchExceptions(x => x.Data.ItemInfo.Prefab == null, e, "Prefab must be not null");
        }
#endif //UNITY_EDITOR
    }
}