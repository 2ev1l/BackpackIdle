using EditorCustom.Attributes;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Universal.Core;
using Universal.Serialization;

namespace Game.DataBase
{
    public class TextData : MonoBehaviour
    {
        #region fields & properties
        public LanguageData DefaultLanguageData => languageData;
        [SerializeField] private LanguageData languageData;
        public static LanguageData EnglishData
        {
            get => englishData;
            set => englishData = value;
        }
        private static LanguageData englishData;
        public static LanguageData LoadedData
        {
            get => loadedData;
            set => loadedData = value;
        }
        private static LanguageData loadedData;
        #endregion fields & properties

        #region methods
        #endregion methods

#if UNITY_EDITOR

        [Button(nameof(ExportToTxt))]
        private void ExportToTxt()
        {
            LanguageData data = LoadedData;
            string path = Application.persistentDataPath + "/export.txt";
            string text = "";

            File.WriteAllText(path, text);
        }
        [Title("Debug")]
        [SerializeField] private string textToFind;
        [SerializeField] private bool debugTextOnValidate;
        [Button(nameof(FindTextEntry))]
        private void FindTextEntry()
        {
            DebugTextEntriesIn(TextType.Menu, languageData.MenuData);
            DebugTextEntriesIn(TextType.Game, languageData.GameData);
        }
        private void DebugTextEntriesIn(TextType type, string[] array)
        {
            FindSameTextIn(array, out List<int> found);
            foreach (int id in found)
            {
                Debug.Log($"<color=#BBFFBB>#{id}</color> - {type} == {array[id]}");
            }
        }
        private void FindSameTextIn(string[] array, out List<int> found)
        {
            found = new();
            string searchText = textToFind;
            for (int i = 0; i < array.Length; ++i)
            {
                if (searchText.Contains(array[i], System.StringComparison.InvariantCultureIgnoreCase))
                {
                    found.Add(i);
                    continue;
                }
                if (array[i].Contains(searchText, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    found.Add(i);
                    continue;
                }
            }
        }
        private void OnValidate()
        {
            if (!debugTextOnValidate) return;
            CheckMultipleArrayEntries(languageData.MenuData, "Menu");
            CheckMultipleArrayEntries(languageData.GameData, "Game");
        }
        private void CheckMultipleArrayEntries(string[] array, string name)
        {
            HashSet<string> equals = array.FindEquals((x, y) => x.Equals(y));
            foreach (var el in equals)
            {
                if (el.Equals("")) continue;
                Debug.LogError($"Multiple text '{el}' in {name}");
            }
        }

#endif //UNITY_EDITOR
    }
}