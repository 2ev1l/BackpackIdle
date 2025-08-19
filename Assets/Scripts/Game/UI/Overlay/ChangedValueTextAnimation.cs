using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class ChangedValueTextAnimation : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private FloatingTextFactory textFactory;
        [SerializeField] private bool doOnIncrease = true;
        [SerializeField] private bool doOnDecrease = true;
        #endregion fields & properties

        #region methods
        public void DoAnimation(int changedValue) => DoAnimation((float)changedValue);
        public void DoAnimation(float changedValue)
        {
            if (!CanDoAnimation(changedValue)) return;
            string text = $"{(changedValue >= 0 ? "+" : "")}{changedValue}";
            textFactory.SpawnText(text);
        }
        public bool CanDoAnimation(float changedValue)
        {
            if (doOnIncrease && changedValue > 0) return true;
            if (doOnDecrease && changedValue < 0) return true;
            return false;
        }
        #endregion methods
    }
}