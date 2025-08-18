using EditorCustom.Attributes;
using UnityEngine;
using Universal.Behaviour;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

namespace Game.UI.Overlay
{
    public class PanelStateChange : StateChange
    {
        #region fields & properties
        public UnityEvent OnPanelActive;
        public UnityEvent OnPanelDisabled;
        [SerializeField] private GameObject panel;
        #endregion fields & properties

        #region methods
        public override void SetActive(bool active)
        {
            if (panel.activeSelf != active)
            {
                panel.SetActive(active);
                if (panel.activeSelf)
                    OnPanelActive?.Invoke();
                else
                    OnPanelDisabled?.Invoke();
            }
        }
        #endregion methods

#if UNITY_EDITOR
        [Button(nameof(SetCurrentObject))]
        private void SetCurrentObject()
        {
            Undo.RecordObject(gameObject, "Set Panel");
            panel = gameObject;
        }
#endif //UNITY_EDITOR
    }
}