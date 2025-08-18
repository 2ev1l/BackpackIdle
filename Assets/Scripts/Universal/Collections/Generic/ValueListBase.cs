using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Universal.Collections.Generic
{
    public abstract class ValueListBase<T> : MonoBehaviour
    {
        #region fields & properties
        public UnityEvent<T> OnIndexChanged;
        public ValueList<T> List => list;
        [SerializeField] private ValueList<T> list;
        [SerializeField] private bool setDataOnEnable;
        [SerializeField] private bool hasControls = true;
        [SerializeField][DrawIf(nameof(hasControls), true)] private Graphic nextIndexRaycast;
        [SerializeField][DrawIf(nameof(hasControls), true)] private Graphic previousIndexRaycast;
        #endregion fields & properties

        #region methods
        protected virtual void OnEnable()
        {
            list.OnIndexChanged += InvokeEvents;
            if (setDataOnEnable)
                SetCustomData();
            list.InvokeAction();
        }
        protected virtual void OnDisable()
        {
            list.OnIndexChanged -= InvokeEvents;
        }
        private void InvokeEvents(int index, T value)
        {
            UpdateUI();
            OnIndexChanged?.Invoke(value);
        }
        protected virtual void UpdateUI()
        {
            if (hasControls)
            {
                nextIndexRaycast.enabled = !list.IsLastIndex;
                previousIndexRaycast.enabled = !list.IsFirstIndex;
            }
        }
        public virtual void SetCustomData() => throw new System.NotImplementedException();
        public void SetNextIndex() => list.Index++;
        public void SetPreviousIndex() => list.Index--;
        #endregion methods

#if UNITY_EDITOR
        [Title("Debug")]
        [SerializeField] private bool previewInEditor = false;
        private void OnValidate()
        {
            list.Validate();
            if (!previewInEditor) return;
            InvokeEvents(list.Index, list.CurrentValue);
        }
#endif //UNITY_EDITOR

    }
}