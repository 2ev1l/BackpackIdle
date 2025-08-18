using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DataBase
{
    public class Entity : MonoBehaviour, IDamageReceiver
    {
        #region fields & properties
        public UnityAction OnDead;
        public UnityAction OnBeforeInitialized;
        public UnityAction OnInitialized;
        public EntityInfo Info => info;
        private EntityInfo info;
        public bool IsDead => Info.Stats.Health.Value == 0;
        #endregion fields & properties

        #region methods
        public virtual void Initialize(EntityInfo info)
        {
            OnBeforeInitialized?.Invoke();
            this.info = info;
            OnInitialized?.Invoke();
        }
        public void Heal(float amount)
        {
            if (amount <= 0)
            {
                Debug.LogError($"Heal must be > 0 ({amount})");
                return;
            }
            Info.Stats.Health.Value += amount;
            Info.Stats.MaxHealth.Value += amount;
        }
        public void ReceiveDamage(float amount)
        {
            if (amount <= 0)
            {
                Debug.LogError($"Damage must be > 0 ({amount})");
                return;
            }
            Info.Stats.Health.SetValueOrZero(Info.Stats.Health.Value - amount);

            if (IsDead)
            {
                OnDead?.Invoke();
            }
        }
        #endregion methods
    }
}