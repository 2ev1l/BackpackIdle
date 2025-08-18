using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    public abstract class Effect : ScriptableObject
    {
        #region fields & properties
        public EffectStatInfo Info => info.Info;
        [SerializeField] private EffectStatInfoSO info;
        public GameObject ParticlesPrefab => particlesPrefab;
        [SerializeField] private GameObject particlesPrefab;
        #endregion fields & properties

        #region methods
        public abstract bool TryActivate(GameObject target, float value);
        #endregion methods
    }
}