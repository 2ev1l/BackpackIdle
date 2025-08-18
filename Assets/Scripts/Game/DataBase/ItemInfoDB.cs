using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class ItemInfoDB : DBInfo
    {
        #region fields & properties
        public ItemInfo ItemInfo => itemInfo;
        [SerializeField] private ItemInfo itemInfo;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}