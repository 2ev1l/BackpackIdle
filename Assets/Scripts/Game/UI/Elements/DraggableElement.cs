using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.UI.Elements
{
    public class DraggableElement : CustomButton, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region fields & properties
        public UnityEvent<PointerEventData> OnBeginDragEvent => onBeginDragEvent;
        [Title("Drag")] [SerializeField] private UnityEvent<PointerEventData> onBeginDragEvent;
        public UnityEvent<PointerEventData> OnDragEvent => onDragEvent;
        [SerializeField] private UnityEvent<PointerEventData> onDragEvent;
        public UnityEvent<PointerEventData> OnEndDragEvent => onEndDragEvent;
        [SerializeField] private UnityEvent<PointerEventData> onEndDragEvent;
        #endregion fields & properties

        #region methods
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDragEvent?.Invoke(eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            onDragEvent?.Invoke(eventData);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            onEndDragEvent?.Invoke(eventData);
        }
        #endregion methods
    }
}