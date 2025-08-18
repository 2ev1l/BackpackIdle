using TMPro;
using UnityEngine;
using Universal.Core;

namespace Universal.Serialization
{
    [System.Serializable]
    public class LanguageSettings : ICloneable<LanguageSettings>
    {
        #region fields & properties
        public const string DEFAULT_LANGUAGE = "English";
        [SerializeField] private string choosedLanguage = DEFAULT_LANGUAGE;
        public string ChoosedLanguage { get => choosedLanguage; }
        #endregion fields & properties

        #region methods
        public void ResetLanguage() => choosedLanguage = DEFAULT_LANGUAGE;
        public LanguageSettings() { }
        public LanguageSettings(string choosedLanguage)
        {
            this.choosedLanguage = choosedLanguage;
        }
        public LanguageSettings Clone()
        {
            return new(choosedLanguage);
        }
        #endregion methods
    }
}