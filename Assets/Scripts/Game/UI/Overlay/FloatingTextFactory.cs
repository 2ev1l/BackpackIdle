using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;

namespace Game.UI.Overlay
{
    public class FloatingTextFactory : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private ObjectPool<DestroyablePoolableObject> texts;
        #endregion fields & properties

        #region methods
        public void SpawnText(string text)
        {
            FloatingText obj = (FloatingText)texts.GetObject();
            obj.transform.position = texts.ParentForSpawn.transform.position;
            obj.Initialize(text);
        }
        #endregion methods
    }
}