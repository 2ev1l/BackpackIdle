using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Universal.Collections;
using Universal.Core;

namespace Game.DataBase
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        #region fields & properties
        public string Name => nameInfo.Text;
        [SerializeField] private LanguageInfo nameInfo;
        public ItemEffectBinding EffectBinding => effectBinding;
        [SerializeField] private ItemEffectBinding effectBinding;
        internal IReadOnlyList<ItemEffectLevelInfo> LevelsInfo => levelsInfo;
        [SerializeField] private List<ItemEffectLevelInfo> levelsInfo;
        public EffectStatInfo ActivationDelayInfo => activationDelayInfo.Info;
        [SerializeField] private EffectStatInfoSO activationDelayInfo;
        [SerializeField][Min(0f)] private float activationDelay = 1f;
        public bool IsAutoActivatable => isAutoActivatable;
        [SerializeField] private bool isAutoActivatable = false;
        /// <summary>
        /// SET IS ONLY FOR EDITOR
        /// </summary>
        public StaticPoolableObject Prefab
        {
            get => prefab;
#if UNITY_EDITOR
            set => prefab = value;
#endif
        }
        [SerializeField] private StaticPoolableObject prefab;
        public Shape Shape => shape;
        [SerializeField] private Shape shape;
        public int MaxLevel => levelsInfo.Count;
        #endregion fields & properties

        #region methods
        public ItemEffectLevelInfo GetLevelInfo(int level) => levelsInfo[level - 1];
        public bool CanUpgrade(int currentLevel) => currentLevel < MaxLevel;
        public bool IsLevelValid(int level)
        {
            int index = level - 1;
            return index >= 0 && index < levelsInfo.Count;
        }

        public int GetValueForLevel(int level)
        {
            int index = level - 1;
            if (IsLevelValid(level))
                return levelsInfo[index].EffectValue;
            Debug.LogError($"Invalid level {level}");
            return 0;
        }

        public bool TryActivate(GameObject activator, GameObject enemy, GameObject skill, int level)
        {
            int value = GetValueForLevel(level);
            return effectBinding.TryActivate(activator, enemy, skill, value);
        }
        #endregion methods
    }
}