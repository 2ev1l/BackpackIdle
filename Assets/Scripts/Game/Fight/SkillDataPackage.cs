using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    [System.Serializable]
    public class SkillDataPackage
    {
        #region fields & properties
        public ItemData ItemData => itemData;
        private ItemData itemData;
        public ISkillTargetProvider TargetProvider => targetProvider;
        private ISkillTargetProvider targetProvider;
        #endregion fields & properties

        #region methods
        public SkillDataPackage(ItemData itemData, ISkillTargetProvider targetProvider)
        {
            this.itemData = itemData;
            this.targetProvider = targetProvider;
        }
        #endregion methods
    }
}