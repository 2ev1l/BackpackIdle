using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [CreateAssetMenu(fileName = "EffectStatInfoSO", menuName = "ScriptableObjects/EffectStatInfoSO")]
    public class EffectStatInfoSO : ScriptableObject
    {
        #region fields & properties
        public EffectStatInfo Info => info;
        [SerializeField] private EffectStatInfo info;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}