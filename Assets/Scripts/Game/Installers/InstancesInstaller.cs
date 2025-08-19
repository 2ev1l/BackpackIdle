using Game.Fight;
using Game.UI.Overlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;
using Zenject;

namespace Game.Installers
{
    public class InstancesInstaller : MonoInstaller
    {
        #region fields & properties
        [SerializeField] private PlayerInventoryInstance playerInventory;
        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            InstallPlayerInventory();
        }
        private void InstallPlayerInventory()
        {
            Container.Bind<PlayerInventoryInstance>().FromInstance(playerInventory).AsSingle().NonLazy();
            Container.QueueForInject(playerInventory);
            playerInventory.ForceInitialize();
        }
        #endregion methods
    }
}