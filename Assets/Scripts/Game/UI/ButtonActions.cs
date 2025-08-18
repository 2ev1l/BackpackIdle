using EditorCustom.Attributes;
using Game.Serialization;
using UnityEngine;
using Universal.Behaviour;

namespace Game.UI
{
    public class ButtonActions : MonoBehaviour
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void LoadScene(string sceneName) => SceneLoader.LoadScene(sceneName);
        [SerializedMethod]
        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
        [SerializedMethod]
        public void SaveImmediate()
        {
            SavingController.Instance.SaveGameData();
        }
        [SerializedMethod]
        public void Exit()
        {
            Application.Quit();
        }
        [SerializedMethod]
        public void LoadGame()
        {
            SceneLoader.LoadScene("Game");
        }

        [SerializedMethod]
        public void ResetTotalProgress()
        {
            SavingController.Instance.ResetTotalProgress();
        }
        [SerializedMethod]
        public void StartNewGame()
        {
            ResetTotalProgress();
            LoadGame();
        }

        [SerializedMethod]
        public void ExitToMenu()
        {
            SceneLoader.LoadScene("Menu");
        }
        #endregion methods
    }
}