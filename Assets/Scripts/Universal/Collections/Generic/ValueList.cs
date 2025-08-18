using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Universal.Collections.Generic
{
    [System.Serializable]
    public class ValueList<T>
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - Index;<br></br>
        /// <see cref="{T1}"/> - Value
        /// </summary>
        public UnityAction<int, T> OnIndexChanged;
        public IReadOnlyList<T> Data => data;
        [SerializeField] private List<T> data = new();
        /// <summary>
        /// Clamps value between min and max data count
        /// </summary>
        public int Index
        {
            get => index;
            set => SetIndex(value);
        }
        [SerializeField][Min(0)] private int index = 0;
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public T CurrentValue => data[index];
        public bool IsFirstIndex => index == 0;
        public bool IsLastIndex => index == Mathf.Max(data.Count - 1, 0);
        #endregion fields & properties

        #region methods
        private void SetIndex(int value)
        {
            index = Mathf.Clamp(index, 0, data.Count - 1);
            index = value;
            InvokeAction();
        }
        public void SetData(IEnumerable<T> data)
        {
            this.data.Clear();
            foreach (T el in data)
            {
                this.data.Add(el);
            }
            InvokeAction();
        }
        public void InvokeAction() => OnIndexChanged?.Invoke(index, data[index]);
        public ValueList() { }
        public ValueList(IEnumerable<T> data)
        {
            foreach (T el in data)
            {
                this.data.Add(el);
            }
        }
        #endregion methods

#if UNITY_EDITOR
        /// <summary>
        /// EDITOR ONLY
        /// </summary>
        public void Validate()
        {
            index = Mathf.Clamp(index, 0, data.Count - 1);
        }
#endif //UNITY_EDITOR

    }
}