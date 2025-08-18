using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class ItemEffectLevelInfo
    {
        #region fields & properties
        public Sprite ItemIcon => itemIcon;
        [SerializeField] private Sprite itemIcon;
        public int EffectValue => effectValue;
        [SerializeField] private int effectValue;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}