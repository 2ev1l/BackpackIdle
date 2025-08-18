using Game.UI.Overlay;
using UnityEngine;
using Zenject;

namespace ProjectInstallers
{
    public class OverlayInstaller : MonoInstaller
    {
        #region fields & properties
        [SerializeField] private ScreenFade screenFade;
        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            InstallScreenFade();
        }
        private void InstallScreenFade()
        {
            Container.BindInterfacesAndSelfTo<ScreenFade>().FromInstance(screenFade).AsSingle();
            Container.QueueForInject(screenFade);
        }
        #endregion methods
    }
}