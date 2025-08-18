using EditorCustom.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SliderActions : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private Transform refObject;
        private Image Image
        {
            get
            {
                if (image == null)
                    image = refObject.GetComponent<Image>();
                return image;
            }
        }
        private Image image;
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void MoveTransformX(float value) => MoveX(SetPosition, value);
        [SerializedMethod]
        public void MoveTransformY(float value) => MoveY(SetPosition, value);
        [SerializedMethod]
        public void MoveTransformZ(float value) => MoveZ(SetPosition, value);
        [SerializedMethod]
        public void MoveTransformLocalX(float value) => MoveX(SetLocalPosition, value);
        [SerializedMethod]
        public void MoveTransformLocalY(float value) => MoveY(SetLocalPosition, value);
        [SerializedMethod]
        public void MoveTransformLocalZ(float value) => MoveZ(SetLocalPosition, value);
        [SerializedMethod]
        public void MoveTransformAnchoredX(float value) => MoveX(SetAnchoredPosition, value);
        [SerializedMethod]
        public void MoveTransformAnchoredY(float value) => MoveY(SetAnchoredPosition, value);
        [SerializedMethod]
        public void MoveTransformAnchoredZ(float value) => MoveZ(SetAnchoredPosition, value);

        [SerializedMethod]
        public void RotateTransformEulerX(float value) => RotateX(SetAngles, value);
        [SerializedMethod]
        public void RotateTransformEulerY(float value) => RotateY(SetAngles, value);
        [SerializedMethod]
        public void RotateTransformEulerZ(float value) => RotateZ(SetAngles, value);
        [SerializedMethod]
        public void RotateTransformLocalEulerX(float value) => RotateX(SetLocalAngles, value);
        [SerializedMethod]
        public void RotateTransformLocalEulerY(float value) => RotateY(SetLocalAngles, value);
        [SerializedMethod]
        public void RotateTransformLocalEulerZ(float value) => RotateZ(SetLocalAngles, value);

        [SerializedMethod]
        public void ImageSetAlpha(float value) => SetAlphaImage(value);
        [SerializedMethod]
        public void ImageSetOneMinusAlpha(float value) => SetAlphaImage(1f - Mathf.Abs(value));

        private void SetAlphaImage(float alpha)
        {
            Color col = Image.color;
            col.a = alpha;
            Image.color = col;
        }

        private void RotateX(Action<Func<Vector3, Vector3>> changeMethod, float value) => changeMethod.Invoke(a => { a.x = value; return a; });
        private void RotateY(Action<Func<Vector3, Vector3>> changeMethod, float value) => changeMethod.Invoke(a => { a.y = value; return a; });
        private void RotateZ(Action<Func<Vector3, Vector3>> changeMethod, float value) => changeMethod.Invoke(a => { a.z = value; return a; });
        private void SetAngles(Func<Vector3, Vector3> changeAngles) => SetBaseAngles(refObject.eulerAngles, ApplyEulerAngles, changeAngles);
        private void SetLocalAngles(Func<Vector3, Vector3> changeAngles) => SetBaseAngles(refObject.localEulerAngles, ApplyLocalEulerAngles, changeAngles);
        private void SetBaseAngles(Vector3 angles, Action<Vector3> setAngles, Func<Vector3, Vector3> changeAngles)
        {
            Vector3 a = changeAngles.Invoke(angles);
            setAngles.Invoke(a);
        }
        private void ApplyEulerAngles(Vector3 newAngles) => refObject.eulerAngles = newAngles;
        private void ApplyLocalEulerAngles(Vector3 newAngles) => refObject.localEulerAngles = newAngles;

        private void MoveZ(Action<Func<Vector3, Vector3>> changeMethod, float value) => changeMethod.Invoke(pos => { pos.z = value; return pos; });
        private void MoveY(Action<Func<Vector3, Vector3>> changeMethod, float value) => changeMethod.Invoke(pos => { pos.y = value; return pos; });
        private void MoveX(Action<Func<Vector3, Vector3>> changeMethod, float value) => changeMethod.Invoke(pos => { pos.x = value; return pos; });
        private void SetAnchoredPosition(Func<Vector3, Vector3> changePosition) => SetBasePosition(((RectTransform)refObject).anchoredPosition3D, ApplyAnchoredPosition, changePosition);
        private void SetPosition(Func<Vector3, Vector3> changePosition) => SetBasePosition(refObject.position, ApplyPosition, changePosition);
        private void SetLocalPosition(Func<Vector3, Vector3> changePosition) => SetBasePosition(refObject.localPosition, ApplyLocalPosition, changePosition);
        private void SetBasePosition(Vector3 objectPosition, Action<Vector3> setPosition, Func<Vector3, Vector3> changePosition)
        {
            Vector3 pos = changePosition.Invoke(objectPosition);
            setPosition.Invoke(pos);
        }
        private void ApplyAnchoredPosition(Vector3 newPosition)
        {
            ((RectTransform)refObject).anchoredPosition3D = newPosition;
        }
        private void ApplyPosition(Vector3 newPosition)
        {
            refObject.position = newPosition;
        }
        private void ApplyLocalPosition(Vector3 newPosition)
        {
            refObject.localPosition = newPosition;
        }
        #endregion methods
    }
}