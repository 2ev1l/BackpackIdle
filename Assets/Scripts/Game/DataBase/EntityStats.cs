using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;

namespace Game.DataBase
{
    [System.Serializable]
    public class EntityStats : ICloneable<EntityStats>
    {
        #region fields & properties
        public Health MaxHealth => maxHealth;
        [SerializeField] private Health maxHealth = new(100);
        public Health Health => health;
        [SerializeField] private Health health = new(100);
        #endregion fields & properties

        #region methods
        public EntityStats Clone() => new(maxHealth.Value, health.Value);
        public EntityStats(float maxHealth, float health)
        {
            this.maxHealth = new(maxHealth);
            this.health = new(health);
        }
        public EntityStats() { }
        #endregion methods
    }
}