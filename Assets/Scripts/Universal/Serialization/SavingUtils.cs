using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Universal.Serialization
{
    public abstract class SavingUtils : MonoBehaviour
    {
        #region fields & properties
        public static SavingUtils Instance => instance;
        private static SavingUtils instance;
        public static UnityAction OnBeforeSave;
        public static UnityAction OnAfterSave;
        public static UnityAction OnDataReset;
        public static UnityAction OnSettingsReset;
        public static string StreamingAssetsPath => Application.dataPath + "/StreamingAssets";
        #endregion fields & properties

        #region methods
        public void ForceInitialize()
        {
            instance = this;
            CheckSaves();
            LoadAll();
        }
        protected abstract void CheckSaves();
        public static bool IsFileExistsInPP(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        public static bool IsFileExists(string dataPath, string fullFileName)
        {
            return File.Exists(Path.Combine(dataPath, fullFileName));
        }
        public void Awake()
        {
            SaveAll();
        }
        protected void SaveAll()
        {
            if (CanSave())
                SaveGameData();
            SaveSettings();
        }
        private void LoadAll()
        {
            LoadGameData();
            LoadSettings();
        }
        public virtual bool CanSave()
        {
            return true;
        }
        public abstract void SaveGameData();
        public abstract void SaveSettings();

        protected abstract void LoadGameData();
        protected abstract void LoadSettings();

        public abstract void ResetTotalProgress(bool doAction = true);

        public static void SaveJsonToPP<T>(string key, T data)
        {
            string json = JsonUtility.ToJson(data, true);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
        public static T LoadJsonFromPP<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
                return default;

            string json = PlayerPrefs.GetString(key);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        }

        public static void SaveJson<T>(string dataPath, T data, string saveName)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(dataPath, saveName);
            File.WriteAllText(path, json);
        }
        public static T LoadJson<T>(string dataPath, string saveName)
        {
            string path = Path.Combine(dataPath, saveName);
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        private void OnDestroy()
        {
            SaveAll();
        }
        public void OnApplicationQuit()
        {
            SaveAll();
        }
        public void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveAll();
            }
        }

        #endregion methods
    }
}