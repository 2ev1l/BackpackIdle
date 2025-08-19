using EditorCustom.Attributes;
using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Universal.Core;
using Universal.Time;

namespace Game.Fight
{
    public class ItemSkillActivator : MonoBehaviour
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> enemy
        /// </summary>
        public UnityAction<GameObject> OnSkillActivated;
        /// <summary>
        /// <see cref="{T0}"/> target
        /// </summary>
        public UnityAction<GameObject> OnSkillDamagedTarget;

        [SerializeField] private Image cooldownProgressImage;
        private readonly TimeDelayContinuous activationDelay = new();
        private ItemInfo ItemInfo => dataPackage.ItemData.Info.ItemInfo;
        public SkillDataPackage DataPackage => dataPackage;
        private SkillDataPackage dataPackage;
        public GameObject Skill => gameObject;
        #endregion fields & properties

        #region methods
        public void Initialize(SkillDataPackage dataPackage)
        {
            UnSubscribe();
            this.dataPackage = dataPackage;
            activationDelay.Delay = ItemInfo.ActivationDelay;

            StopActivate();
            Subscribe();
        }
        private void OnEnable()
        {
            Subscribe();
        }
        private void OnDisable()
        {
            StopActivate();
            UnSubscribe();
            CancelInvoke();
        }
        private void Subscribe()
        {
            if (dataPackage == null) return;
            activationDelay.OnTimeLasts += UpdateProgressUI;
            if (ItemInfo.IsAutoActivatable)
                activationDelay.OnDelayReady += ActivateSkill;
        }
        private void UnSubscribe()
        {
            if (dataPackage == null) return;
            activationDelay.OnTimeLasts -= UpdateProgressUI;
            activationDelay.OnDelayReady -= ActivateSkill;
        }
        private void UpdateProgressUI(float timeLasts)
        {
            float scaledTime = timeLasts / activationDelay.Delay;
            cooldownProgressImage.fillAmount = scaledTime;
        }

        public void StopActivate()
        {
            activationDelay.TryBreakDelaying();
            UpdateProgressUI(0);
        }
        public void ActivateSkillDelayed(float delay)
        {
            Invoke(nameof(ActivateSkill), delay);
        }
        public void ActivateSkill()
        {
            if (!activationDelay.CanActivate) return;
            if (!gameObject.activeInHierarchy) return;
            Invoke(ItemInfo.ActivationMethod, 0);
            activationDelay.Activate();
        }

        private void ActivateWeaponSkill()
        {
            WeaponInfo weaponInfo = ItemInfo as WeaponInfo;
            WeaponBullet bullet = WeaponBulletFactory.Instance.SpawnBullet(weaponInfo.GetProjectileIcon(dataPackage.ItemData.Level));
            Vector3 skillGlobalPos = Skill.transform.position;
            bullet.transform.position = skillGlobalPos;
            GameObject target = ItemInfo.EffectBinding.TargetType.DefineTarget(dataPackage.TargetProvider.Activator, dataPackage.TargetProvider.FindEnemy(ItemInfo), Skill);
            bullet.MoveTo(target, 1f / weaponInfo.ProjectileSpeed, OnBulletMovedToEnemy);
            OnSkillActivated?.Invoke(target);
        }
        private void OnBulletMovedToEnemy(GameObject target, WeaponBullet bullet)
        {
            ItemInfo.TryActivate(dataPackage.TargetProvider.Activator, target, Skill, dataPackage.ItemData.Level);
            bullet.DisableObject();
            OnSkillDamagedTarget?.Invoke(target);
        }
        private void ActivateItemSkill()
        {
            GameObject enemy = dataPackage.TargetProvider.FindEnemy(ItemInfo);
            ItemInfo.TryActivate(dataPackage.TargetProvider.Activator, enemy, Skill, dataPackage.ItemData.Level);
            OnSkillActivated?.Invoke(enemy);
        }
        #endregion methods
    }
}