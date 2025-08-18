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
        public EntityStats Info => info;
        private EntityStats info;
        public bool IsDead => Info.Health.Value == 0;
        #endregion fields & properties

        #region methods
        public virtual void Initialize(EntityStats info)
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
            Info.Health.Value += amount;
            Info.MaxHealth.Value += amount;
        }
        public void ReceiveDamage(float amount)
        {
            if (amount <= 0)
            {
                Debug.LogError($"Damage must be > 0 ({amount})");
                return;
            }
            Info.Health.SetValueOrZero(Info.Health.Value - amount);

            if (IsDead)
            {
                OnDead?.Invoke();
            }
        }
        #endregion methods
    }
}