using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class EffectStatInfo
    {
        #region fields & properties
        public string Name => name.Text;
        [SerializeField] private LanguageInfo name;
        public Sprite Icon => icon;
        [SerializeField] private Sprite icon;
        #endregion fields & properties

        #region methods
        public string GetEffectText(float value)
        {
            return value.ToString();
        }
        #endregion methods
    }
}