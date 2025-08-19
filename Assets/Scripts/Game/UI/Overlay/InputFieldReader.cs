using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Universal.Core;

namespace Game.UI.Overlay
{
    public class InputFieldReader : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private TMP_InputField inputField;
        public UnityEvent<int> OnIntRead;
        #endregion fields & properties

        #region methods
        private string GetText() => inputField.text;
        public void ReadInt()
        {
            int value = 0;
            try { value = System.Convert.ToInt32(GetText()); }
            catch { }
            OnIntRead?.Invoke(value);
        }
        #endregion methods
    }
}