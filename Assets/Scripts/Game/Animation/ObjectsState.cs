using EditorCustom.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Animation
{
    public class ObjectsState : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private List<GameObject> objects;
        [SerializeField] private bool currentState = false;
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void FlipState()
        {
            ChangeObjectsState(!currentState);
        }
        [SerializedMethod]
        public void EnableObjects() => ChangeObjectsState(true);
        [SerializedMethod]
        public void DisableObjects() => ChangeObjectsState(false);
        private void ChangeObjectsState(bool state)
        {
            currentState = state;
            objects.ForEach(x => x.SetActive(state));
        }
        [SerializedMethod]
        public void SetActiveNext()
        {
            GameObject activeObject = objects.Find(x => x.activeSelf);
            int activeObjectIndex = objects.IndexOf(activeObject);
            if (activeObjectIndex == objects.Count - 1) return;
            objects[activeObjectIndex + 1].SetActive(true);
        }
        [SerializedMethod]
        public void DisableFirstActive()
        {
            GameObject activeObject = objects.Find(x => x.activeSelf);
            activeObject.SetActive(false);
        }
        #endregion methods
    }
}