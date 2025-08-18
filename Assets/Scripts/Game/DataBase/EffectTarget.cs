using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    #region enum
    public enum EffectTarget
    {
        Activator,
        Enemy,
        Skill
    }
    #endregion enum

    public static class EffectTargetExtension
    {
        #region methods
        public static GameObject DefineTarget(this EffectTarget et, GameObject activator, GameObject enemy, GameObject skill) => et switch
        {
            EffectTarget.Activator => activator,
            EffectTarget.Enemy => enemy,
            EffectTarget.Skill => skill,
            _ => throw new System.NotImplementedException($"effect target for {et}")
        };
        #endregion methods
    }
}