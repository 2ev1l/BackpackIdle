using Game.Audio;
using Game.Events;
using Game.UI.Overlay;
using UnityEngine;
using Universal.Behaviour;
using Universal.Events;
using Zenject;

namespace Game.ProjectInstallers
{
    public class SettingsInstaller : MonoInstaller
    {
        #region fields & properties
        [SerializeField] private SingleGameInstance singleGameInstance;
        [SerializeField] private MessageController messageController;
        [SerializeField] private CursorController cursorController;
        [SerializeField] private AudioManager audioManager;
        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            InstallSingleGameInstance();
            InstallMessageController();
            InstallCursorSettings();
            InstallAudioManager();
        }
        private void InstallSingleGameInstance()
        {
            Container.BindInterfacesAndSelfTo<SingleGameInstance>().FromInstance(singleGameInstance).AsSingle();
            singleGameInstance.ForceInitialize();
        }
        private void InstallAudioManager()
        {
            Container.BindInterfacesAndSelfTo<AudioManager>().FromInstance(audioManager).AsSingle();
        }

        private void InstallMessageController()
        {
            Container.BindInterfacesAndSelfTo<MessageController>().FromInstance(messageController).AsSingle();
        }
        private void InstallCursorSettings()
        {
            Container.BindInterfacesAndSelfTo<CursorController>().FromInstance(cursorController).AsSingle();
        }
        #endregion methods
    }
}