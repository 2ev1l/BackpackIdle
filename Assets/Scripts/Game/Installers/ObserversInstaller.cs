using Game.UI.Overlay;
using UnityEngine;
using Universal.Behaviour;
using Zenject;

namespace Game.Installers
{
    public class ObserversInstaller : MonoInstaller
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            
        }
        private void BindObserverFromInstance<T>(T instance) where T : Observer
        {
            Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
            Container.QueueForInject(instance);
        }
        #endregion methods

    }
}