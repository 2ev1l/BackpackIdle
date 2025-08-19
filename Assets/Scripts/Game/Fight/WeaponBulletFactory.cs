using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;

namespace Game.Fight
{
    [System.Serializable]
    public class WeaponBulletFactory : Zenject.IInitializable
    {
        #region fields & properties
        public static WeaponBulletFactory Instance { get; private set; }
        [SerializeField] private ObjectPool<StaticPoolableObject> bulletPool;
        #endregion fields & properties

        #region methods
        public void Initialize()
        {
            Instance = this;
        }
        public WeaponBullet SpawnBullet(Sprite sprite)
        {
            WeaponBullet bullet = bulletPool.GetObject() as WeaponBullet;
            bullet.Initialize(sprite);
            return bullet;
        }
        #endregion methods
    }
}