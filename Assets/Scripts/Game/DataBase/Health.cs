using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class Health : Stat
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public Health(float value) : base(value) { }
        #endregion methods
    }
}