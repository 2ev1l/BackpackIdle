using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Game.DataBase
{
    #region enum
    public enum TextType
    {
        None,
        Menu,
        Game
    }
    #endregion enum

    public static class TextTypeExtension
    {
        #region methods
        public static string GetRawText(this TextType textType, int id)
        {
            if (id < 0) return "";
            string text;
            try { text = GetTextFromLanguage(textType, id, TextData.LoadedData); }
            catch { text = GetTextFromLanguage(textType, id, TextData.EnglishData); }
            return text;
        }
        private static string GetTextFromLanguage(TextType textType, int id, LanguageData data) => (textType) switch
        {
            TextType.None => "",
            TextType.Menu => data.MenuData[id],
            TextType.Game => data.GameData[id],
            _ => throw new System.NotImplementedException($"Text Type {textType}"),
        };
        #endregion methods
    }
}