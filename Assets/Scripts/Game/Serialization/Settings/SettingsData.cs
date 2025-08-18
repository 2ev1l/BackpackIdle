using UnityEngine;
using UnityEngine.Events;
using Universal.Serialization;
using AudioSettings = Universal.Serialization.AudioSettings;

namespace Game.Serialization.Settings
{
    [System.Serializable]
    public class SettingsData
    {
        #region fields & properties
        public static readonly string SaveName = "settings";
        public static readonly string SaveExtension = ".json";
        public static SettingsData Data => data;
        private static SettingsData data;

        public UnityAction<LanguageSettings> OnLanguageChanged;

        [SerializeField] private LanguageSettings languageSettings = new();

        [SerializeField] private AudioSettings audioSettings = new();
        [SerializeField] private bool isFirstLaunch = true;

        public LanguageSettings LanguageSettings
        {
            get => languageSettings;
            set => SetLanguage(value);
        }
        public AudioSettings AudioSettings
        {
            get => audioSettings;
        }

        #endregion fields & properties

        #region methods
        private void SetLanguage(LanguageSettings value)
        {
            languageSettings = value;
            OnLanguageChanged?.Invoke(value);
        }
        public static void SetData(SettingsData value) => data = value;
        #endregion methods
    }
}