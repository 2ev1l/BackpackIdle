using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;

namespace Game.DataBase
{
    [System.Serializable]
    public class ItemEffectBinding
    {
        #region fields & properties
        public Effect Effect => effect;
        [SerializeField] private Effect effect;
        public EffectTarget TargetType => targetType;
        [SerializeField] private EffectTarget targetType;
        public EffectParticles Particles => particles;
        [SerializeField] private EffectParticles particles;
        #endregion fields & properties

        #region methods
        public bool TryActivate(GameObject activator, GameObject enemy, GameObject skill, float value)
        {
            GameObject target = targetType.DefineTarget(activator, enemy, skill);
            return effect.TryActivate(target, value);
        }
        #endregion methods
    }
}