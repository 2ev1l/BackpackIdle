using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;

namespace Game.DataBase
{
    [System.Serializable]
    public class EffectParticles
    {
        #region fields & properties
        public DestroyablePoolableObject Prefab => prefab;
        [SerializeField] private DestroyablePoolableObject prefab;
        public EffectTarget Target => target;
        [SerializeField] private EffectTarget target;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}