using Game.Serialization.World;
using Game.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Fight
{
    [RequireComponent(typeof(DraggableElement))]
    public class ItemMove : MonoBehaviour
    {
        #region fields & properties
        public UnityAction OnMoveStart;
        public UnityAction OnMoveEnd;
        public UnityAction OnMoving;
        public static bool CanMoveObjects = true;

        private DraggableElement DraggableElement
        {
            get
            {
                if (draggableElement == null)
                    draggableElement = GetComponent<DraggableElement>();
                return draggableElement;
            }
        }
        private DraggableElement draggableElement;
        private Camera ActiveCamera
        {
            get
            {
                if (activeCamera == null)
                    activeCamera = Camera.main;
                return activeCamera;
            }
        }
        private Camera activeCamera = null;
        private RectTransform ItemsParent
        {
            get
            {
                if (itemsParent == null)
                    itemsParent = PlayerInventoryInstance.Instance.ItemsParent;
                return itemsParent;
            }
        }
        private RectTransform itemsParent = null;

        private Transform lastParent = null;
        private Vector2 lastPosition = Vector2.zero;
        private Vector2 startMoveOffset = Vector2.zero;
        public bool isMoveStarted = false;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            DraggableElement.OnBeginDragEvent.AddListener(StartMove);
            DraggableElement.OnDragEvent.AddListener(MoveItem);
            DraggableElement.OnEndDragEvent.AddListener(EndMove);
        }
        private void OnDisable()
        {
            DraggableElement.OnBeginDragEvent.RemoveListener(StartMove);
            DraggableElement.OnDragEvent.RemoveListener(MoveItem);
            DraggableElement.OnEndDragEvent.RemoveListener(EndMove);

            if (isMoveStarted)
            {
                OnMoveEnd?.Invoke();
                CanMoveObjects = true;
            }
        }
        private void StoreMoveInfo(PointerEventData e)
        {
            lastParent = transform.parent;
            lastPosition = ((RectTransform)transform).anchoredPosition;
            bool hit = TryGetLocalPointInRectangle(ItemsParent, e, out Vector2 localPoint);
            if (hit)
            {
                startMoveOffset = lastPosition - localPoint;
            }
        }
        public void ResetPositionToDefaultParent()
        {
            transform.SetParent(PlayerInventoryInstance.Instance.ItemsDefaultParent);
        }
        public void ResetPosition()
        {
            transform.SetParent(lastParent, true);
            ((RectTransform)transform).anchoredPosition = lastPosition;
        }
        private void SetBackpackParent()
        {
            transform.SetParent(ItemsParent, true);
        }
        public void StartMove(PointerEventData e)
        {
            if (!CanMoveObjects) return;
            CanMoveObjects = false;
            isMoveStarted = true;
            SetBackpackParent();
            StoreMoveInfo(e);
            OnMoveStart?.Invoke();
        }
        public void EndMove(PointerEventData e)
        {
            if (!isMoveStarted) return;
            isMoveStarted = false;
            CanMoveObjects = true;

            OnMoveEnd?.Invoke();
        }
        public bool TryGetLocalPointInRectangle(RectTransform parent, PointerEventData e, out Vector2 localPoint)
        {
            return RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, e.position, ActiveCamera, out localPoint);
        }
        public void MoveItem(PointerEventData e)
        {
            if (!isMoveStarted) return;
            bool isHit = TryGetLocalPointInRectangle(ItemsParent, e, out Vector2 localPoint);
            if (!isHit) return;
            ((RectTransform)transform).anchoredPosition = localPoint + startMoveOffset;
            OnMoving?.Invoke();
        }
        #endregion methods
    }
}