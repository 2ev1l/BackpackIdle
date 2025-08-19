using Game.DataBase;
using Game.UI.Overlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;

namespace Game.Fight
{
    public class EntityParticles : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private DestroyablePoolableObject damagedParticles;
        [SerializeField] private Entity entity;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            entity.OnDamaged.AddListener(OnEntityDamaged);
        }
        private void OnDisable()
        {
            entity.OnDamaged.RemoveListener(OnEntityDamaged);
        }
        private void OnEntityDamaged(float _) => OnEntityDamaged();
        private void OnEntityDamaged()
        {
            ParticlesFactory.Instance.SpawnParticle(damagedParticles, entity.transform.position);
        }
        #endregion methods
    }
}