using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.DataBase;
using System.Linq;
using Universal.Core;
using Universal.Serialization;
using Universal.Behaviour;
using Game.Serialization.Settings;

namespace Game.UI.Text
{
    [System.Serializable]
    public class TextObserver : Observer
    {
        #region fields & properties
        [SerializeField] private TextData textData;
        #endregion fields & properties

        #region methods
        public override void Dispose()
        {
            SettingsData.Data.OnLanguageChanged -= UpdateText;
            SceneLoader.OnSceneLoaded -= WaitForUpdateText;
        }
        public override void Initialize()
        {
            SettingsData.Data.OnLanguageChanged += UpdateText;
            SceneLoader.OnSceneLoaded += WaitForUpdateText;
            TextData.EnglishData = textData.DefaultLanguageData;
            WaitForUpdateText();
        }
        private void WaitForUpdateText() => SingleGameInstance.Instance.StartCoroutine(WaitForUpdateText_IEnumerator());
        private IEnumerator WaitForUpdateText_IEnumerator()
        {
            yield return null;
            UpdateText();
        }
        private void UpdateText(LanguageSettings languageSettings) => UpdateText();
        private void UpdateText()
        {
            LoadChoosedLanguage();
            UpdateTextObjects();
        }
        public void UpdateTextObjects()
        {
            foreach (var el in GameObject.FindObjectsByType<LanguageLoader>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                el.Load();
            }
        }
        public void LoadLanguage()
        {
            SettingsData.Data.LanguageSettings.ResetLanguage();
        }
        public void LoadChoosedLanguage() => LoadLanguage();
        #endregion methods
    }
}