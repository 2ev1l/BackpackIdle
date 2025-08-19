using EditorCustom.Attributes;
using Game.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Serialization;

namespace Game.Fight
{
    public class SerializationController : MonoBehaviour
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void Save() => SavingController.Instance.SaveGameData();
        #endregion methods
    }
}