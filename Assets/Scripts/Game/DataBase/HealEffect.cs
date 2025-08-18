using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = "ScriptableObjects/HealEffect")]
    public class HealEffect : Effect
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override bool TryActivate(GameObject target, float value)
        {
            if (!target.TryGetComponent(out Entity entity)) return false;
            entity.Heal(value);
            return true;
        }
        #endregion methods
    }
}