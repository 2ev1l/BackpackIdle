using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Universal.Collections;
using Universal.Time;

namespace Game.Fight
{
    public class WeaponBullet : StaticPoolableObject
    {
        #region fields & properties
        [SerializeField] private Image icon;
        [SerializeField] private VectorTimeChanger positionChanger = new();
        private System.Action<GameObject, WeaponBullet> onMoveEnd = null;
        private GameObject targetObject = null;
        #endregion fields & properties

        #region methods
        public void Initialize(Sprite sprite)
        {
            icon.sprite = sprite;
            onMoveEnd = null;
            targetObject = null;
        }
        private void FixRotation()
        {
            Vector2 directionToTarget = (Vector2)targetObject.transform.position - (Vector2)transform.position;
            transform.up = directionToTarget;
        }
        public void MoveTo(GameObject target, float time, System.Action<GameObject, WeaponBullet> onMoveEnd)
        {
            this.onMoveEnd = onMoveEnd;
            this.targetObject = target;
            Vector3 finalLocalPos = transform.parent.InverseTransformPoint(target.transform.position);
            finalLocalPos.z = 0;
            positionChanger.SetValues(transform.localPosition, finalLocalPos);
            positionChanger.SetActions(x => transform.localPosition = x, OnMoveEnd);
            positionChanger.Restart(time);
            FixRotation();
        }
        private void OnMoveEnd()
        {
            onMoveEnd?.Invoke(targetObject, this);
        }
        #endregion methods
    }
}