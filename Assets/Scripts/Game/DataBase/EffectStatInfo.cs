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
        [System.NonSerialized] private string description;
        #endregion fields & properties

        #region methods
        public string GetEffectText()
        {
            return description;
        }
        /// <summary>
        /// Will not be serialized.
        /// </summary>
        /// <param name="value"></param>
        internal void SetDescription(string desc) => this.description = desc;
        #endregion methods
    }
}