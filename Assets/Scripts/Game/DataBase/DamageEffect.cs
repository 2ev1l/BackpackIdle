using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "ScriptableObjects/DamageEffect")]
    public class DamageEffect : Effect
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override bool TryActivate(GameObject target, float value)
        {
            if (!target.TryGetComponent(out IDamageReceiver damageReceiver)) return false;
            damageReceiver.ReceiveDamage(value);
            return true;
        }
        #endregion methods
    }
}