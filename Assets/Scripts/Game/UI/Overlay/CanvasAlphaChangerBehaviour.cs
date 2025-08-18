using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class CanvasAlphaChangerBehaviour : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private CanvasAlphaChanger changer;
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void HideCanvas() => changer.HideCanvas();
        [SerializedMethod]
        public void FadeDown(float speed) => changer.Fade(false, speed);
        [SerializedMethod]
        public void FadeUp(float speed) => changer.Fade(true, speed);
        #endregion methods
    }
}