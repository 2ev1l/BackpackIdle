using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Universal.Collections;

namespace Game.DataBase
{
    public class Entity : StaticPoolableObject, IDamageReceiver
    {
        #region fields & properties
        public UnityEvent OnDead;
        /// <summary>
        /// <see cref="{T0}"/> - damage value
        /// </summary>
        public UnityEvent<float> OnDamaged;
        /// <summary>
        /// <see cref="{T0}"/> - heal value
        /// </summary>
        public UnityEvent<float> OnHealed;
        public EntityStats Stats => stats;
        private EntityStats stats;
        public bool IsDead => Stats.Health.Value == 0;
        private bool isSubscribed = false;
        #endregion fields & properties

        #region methods
        protected virtual void OnEnable()
        {
            Subscribe();
        }
        protected virtual void OnDisable()
        {
            UnSubscribe();
        }
        public virtual void Initialize(EntityStats info)
        {
            Subscribe();
            this.stats = info;
            UnSubscribe();
        }
        private void Subscribe()
        {
            if (isSubscribed || Stats == null) return;
            OnSubscribe();
            isSubscribed = true;
        }
        private void UnSubscribe()
        {
            if (!isSubscribed || Stats == null) return;
            OnUnSubscribe();
            isSubscribed = false;
        }
        protected virtual void OnSubscribe() { }
        protected virtual void OnUnSubscribe() { }

        public void Heal(float amount)
        {
            if (amount <= 0)
            {
                Debug.LogError($"Heal must be > 0 ({amount})");
                return;
            }
            Stats.MaxHealth.Value += amount;
            Stats.Health.Value += amount;
            OnHealed?.Invoke(amount);
        }
        public void ReceiveDamage(float amount)
        {
            if (amount <= 0)
            {
                Debug.LogError($"Damage must be > 0 ({amount})");
                return;
            }
            Stats.Health.SetValueOrZero(Stats.Health.Value - amount);
            OnDamaged?.Invoke(amount);
            if (IsDead)
            {
                OnDead?.Invoke();
            }
        }
        #endregion methods
    }
}