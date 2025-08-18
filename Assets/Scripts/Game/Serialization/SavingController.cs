using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Game.Serialization.Settings;
using Universal;
using Universal.Serialization;
using Game.Serialization.World;

namespace Game.Serialization
{
    public class SavingController : SavingUtils
    {
        #region fields & properties
        #endregion fields & properties

        #region methods
        protected override void CheckSaves()
        {
            if (!IsFileExistsInPP(GameData.SaveName)) ResetTotalProgress();
            if (!IsFileExistsInPP(SettingsData.SaveName)) ResetSettings();
        }

        public override void SaveGameData()
        {
            OnBeforeSave?.Invoke();
            SaveJsonToPP(GameData.SaveName, GameData.Data);
            OnAfterSave?.Invoke();
        }
        public override void SaveSettings()
        {
            SaveJsonToPP(SettingsData.SaveName, SettingsData.Data);
        }
        protected override void LoadGameData()
        {
            GameData gd = LoadJsonFromPP<GameData>(GameData.SaveName);
            if (gd == null)
            {
                Debug.Log("Game data reset");
                gd = new();
            }
            GameData.SetData(gd);
        }
        protected override void LoadSettings()
        {
            SettingsData d = LoadJsonFromPP<SettingsData>(SettingsData.SaveName);
            if (d == null)
            {
                Debug.Log("Settings reset");
                d = new();
                SaveJsonToPP(SettingsData.SaveName, d);
            }
            SettingsData.SetData(d);
        }
        private void ResetSettings()
        {
            SettingsData.SetData(new SettingsData());
            SaveSettings();
            OnSettingsReset?.Invoke();
            Debug.Log("Settings reset");
        }
        public override void ResetTotalProgress(bool doAction = true)
        {
            GameData.SetData(new GameData());
            Instance.SaveGameData();
            if (doAction)
                OnDataReset?.Invoke();
            Debug.Log("Progress reset");
        }
        #endregion methods
    }
}