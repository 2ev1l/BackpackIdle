using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class ParticlesFactory : Zenject.IInitializable
    {
        #region fields & properties
        public static ParticlesFactory Instance { get; private set; }
        private readonly Dictionary<DestroyablePoolableObject, ObjectPool<DestroyablePoolableObject>> particlesPool = new();
        [SerializeField] private Transform parentForSpawn;
        #endregion fields & properties

        #region methods
        public void Initialize()
        {
            Instance = this;
        }
        private ObjectPool<DestroyablePoolableObject> GetPool(DestroyablePoolableObject prefab)
        {
            if (!particlesPool.ContainsKey(prefab))
            {
                ObjectPool<DestroyablePoolableObject> pool = new(prefab, parentForSpawn);
                particlesPool.Add(prefab, pool);
                return pool;
            }

            return particlesPool[prefab];
        }
        public void SpawnParticle(DestroyablePoolableObject prefab, Vector3 worldPosition)
        {
            var pool = GetPool(prefab);
            var obj = pool.GetObject();
            obj.transform.position = worldPosition;
        }
        #endregion methods
    }
}