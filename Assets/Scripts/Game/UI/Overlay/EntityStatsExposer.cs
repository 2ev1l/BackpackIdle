using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Overlay
{
    public class EntityStatsExposer : MonoBehaviour
    {
        #region fields & properties
        public Slider HealthSlider => healthSlider;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private ChangedValueTextAnimation healthDecreaseAnimation;
        private bool isSubscribed = false;
        private EntityStats stats;
        #endregion fields & properties

        #region methods
        protected virtual void OnEnable()
        {
            Subscribe();
            UpdateUI();
        }
        protected virtual void OnDisable()
        {
            UnSubscribe();
        }

        public void Initialize(EntityStats stats)
        {
            UnSubscribe();
            this.stats = stats;
            Subscribe();
            UpdateUI();
        }

        private void Subscribe()
        {
            if (stats == null) return;
            if (isSubscribed) return;
            isSubscribed = true;
            stats.Health.OnValueChanged += UpdateHealthUI;
            stats.Health.OnValueChangedAmount += UpdateChangedHealthUI;
            stats.MaxHealth.OnValueChanged += UpdateMaxHealthUI;
        }
        private void UnSubscribe()
        {
            if (stats == null) return;
            if (!isSubscribed) return;
            isSubscribed = false;
            stats.Health.OnValueChanged -= UpdateHealthUI;
            stats.Health.OnValueChangedAmount -= UpdateChangedHealthUI;
            stats.MaxHealth.OnValueChanged -= UpdateMaxHealthUI;
        }
        private void UpdateUI()
        {
            if (stats == null) return;
            UpdateMaxHealthUI();
            UpdateHealthUI();
        }
        private void UpdateChangedHealthUI(float changedAmount)
        {
            if (healthDecreaseAnimation == null) return;
            healthDecreaseAnimation.DoAnimation(changedAmount);
        }
        private void UpdateMaxHealthUI(float _) => UpdateMaxHealthUI();
        private void UpdateMaxHealthUI()
        {
            if (healthSlider == null) return;
            healthSlider.maxValue = stats.MaxHealth.Value;
        }
        private void UpdateHealthUI(float _) => UpdateHealthUI();
        private void UpdateHealthUI()
        {
            if (healthSlider == null) return;
            healthSlider.value = stats.Health.Value;
        }
        #endregion methods
    }
}