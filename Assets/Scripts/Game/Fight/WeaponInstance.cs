using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class WeaponInstance : ItemInstance
    {
        #region fields & properties

        #endregion fields & properties

        #region methods

        #endregion methods
#if UNITY_EDITOR
        [Button(nameof(GeneratePrefabFromInfo))]
        protected override void GeneratePrefabFromInfo()
        {
            base.GeneratePrefabFromInfo();
        }
#endif //UNITY_EDITOR
    }
}