using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;

namespace Game.DataBase
{
    [CreateAssetMenu(fileName = "AttackSpeedEffectInfo", menuName = "ScriptableObjects/AttackSpeedEffectInfo")]
    public class AttackSpeedEffectInfo : EffectStatInfoSO
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        internal override void SetInfoDescription(string cooldown)
        {
            float value = CustomMath.ConvertToFloat(cooldown);
            value = 1f / value;
            base.SetInfoDescription(value.ToString("F1"));
        }
        #endregion methods
    }
}