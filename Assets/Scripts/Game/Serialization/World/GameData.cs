using EditorCustom.Attributes;
using Game.DataBase;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class GameData
    {
        #region fields & properties
        public static readonly string SaveName = "save";
        public static readonly string SaveExtension = ".data";

        public static GameData Data => data;
        private static GameData data;
        #endregion fields & properties

        #region methods
        public static void SetData(GameData value) => data = value;
        #endregion methods
    }
}