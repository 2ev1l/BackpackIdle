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
        [SerializeField] private WeaponBulletFactory bulletFactory;
        [SerializeField] private ParticlesFactory particlesFactory;
        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            InstallPlayerInventory();
            InstallBulletFactory();
            InstallParticlesFactory();
        }
        private void InstallParticlesFactory()
        {
            Container.BindInterfacesAndSelfTo<ParticlesFactory>().FromInstance(particlesFactory).AsSingle().NonLazy();
            Container.QueueForInject(particlesFactory);
        }
        private void InstallBulletFactory()
        {
            Container.BindInterfacesAndSelfTo<WeaponBulletFactory>().FromInstance(bulletFactory).AsSingle().NonLazy();
            Container.QueueForInject(bulletFactory);
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