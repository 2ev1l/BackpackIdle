using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class LanguageData
    {
        #region fields & properties
        public string[] MenuData => menuData;
        [SerializeField][TextArea(0, 30)] private string[] menuData;
        public string[] GameData => gameData;
        [SerializeField][TextArea(0, 30)] private string[] gameData;

        #endregion fields & properties

        #region methods

        #endregion methods
    }
}