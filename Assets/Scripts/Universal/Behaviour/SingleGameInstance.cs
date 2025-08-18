using UnityEngine;

namespace Universal.Behaviour
{
    [ExecuteAlways]
    public class SingleGameInstance : MonoBehaviour
    {
        #region fields & properties
        public static SingleGameInstance Instance => instance;
        private static SingleGameInstance instance;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            if (instance == null)
                ForceInitialize();
        }
        public void ForceInitialize()
        {
            instance = this;
        }
        #endregion methods
    }
}