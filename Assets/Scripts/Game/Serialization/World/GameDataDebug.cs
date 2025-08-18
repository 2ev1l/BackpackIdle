using EditorCustom.Attributes;
using UnityEngine;
using Universal.Serialization;

namespace Game.Serialization.World
{
    internal sealed class GameDataDebug : MonoBehaviour
    {
#if UNITY_EDITOR
        #region fields & properties
        [SerializeField] private GameData Context = null;
        [Title("Debug")]
        [SerializeField] private bool doAutoSave = false;
        [SerializeField][DrawIf(nameof(doAutoSave), true)][Min(1)] private int autoSaveSeconds = 60;
        [SerializeField][ReadOnly] private float storedTime = 0;
        
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            UpdateData();
            SavingUtils.OnDataReset += UpdateData;
        }
        private void OnDisable()
        {
            SavingUtils.OnDataReset -= UpdateData;
        }
        private void Update()
        {
            if (doAutoSave)
            {
                storedTime += Time.deltaTime;
                if (storedTime > autoSaveSeconds)
                {
                    storedTime = 0;
                    SavingUtils.Instance.SaveGameData();
                }
            }
        }
        [Button(nameof(UpdateData))]
        private void UpdateData()
        {
            Context = GameData.Data;
        }
        
        [Button(nameof(SetCurrentDataAsReal))]
        private void SetCurrentDataAsReal()
        {
            GameData.SetData(Context);
        }
        [Button(nameof(SAVEDATA))]
        private void SAVEDATA()
        {
            SavingUtils.Instance.SaveGameData();
        }
        #endregion methods
#endif //UNITY_EDITOR
    }
}