using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Universal.Collections;
using Universal.Time;

namespace Game.UI.Overlay
{
    public class FloatingText : DestroyablePoolableObject
    {
        #region fields & properties
        [SerializeField] private TextMeshProUGUI textInfo;
        [SerializeField] private Transform panel;
        [SerializeField] private VectorTimeChanger positionChanger;
        [SerializeField] private Vector3 positionOffset;
        #endregion fields & properties

        #region methods
        /// <summary>
        /// Use this to initalize floating text
        /// </summary>
        public void Initialize(string text)
        {
            textInfo.text = text;
            positionChanger.SetValues(panel.transform.localPosition, panel.transform.localPosition + positionOffset);
            positionChanger.SetActions(x => panel.transform.localPosition = x);
            positionChanger.Restart(LiveTime);
        }
        #endregion methods
    }
}