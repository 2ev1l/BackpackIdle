using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Serialization.World
{
    [System.Serializable]
    public abstract class DBData<T> where T : DBInfo
    {
        #region fields & properties
        public int Id => id;
        [SerializeField][Min(0)] private int id = 0;
        public T Info
        {
            get
            {
                if (info == null)
                    info = GetInfo();
                return info;
            }
        }
        private T info = null;
        #endregion fields & properties

        #region methods
        protected abstract T GetInfo();

        public DBData(int id)
        {
            this.id = id;
        }
        #endregion methods
    }
}