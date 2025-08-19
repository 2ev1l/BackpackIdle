using Game.DataBase;
using Game.UI.Overlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game.Fight
{
    public class ItemSkillParticles : MonoBehaviour
    {
        #region fields & properties
        private EffectParticles Particles => skillActivator.DataPackage.ItemData.Info.ItemInfo.EffectBinding.Particles;
        [SerializeField] private ItemSkillActivator skillActivator;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            skillActivator.OnSkillActivated += OnSkillActivated;
            skillActivator.OnSkillDamagedTarget += OnSkillDamagedTarget;
        }
        private void OnDisable()
        {
            skillActivator.OnSkillActivated -= OnSkillActivated;
            skillActivator.OnSkillDamagedTarget -= OnSkillDamagedTarget;
        }
        private GameObject DefineTarget(GameObject enemy) => Particles.Target.DefineTarget(skillActivator.DataPackage.TargetProvider.Activator, enemy, skillActivator.Skill);

        private void OnSkillDamagedTarget(GameObject target)
        {
            if (Particles.Target != EffectTarget.Enemy) return;
            ParticlesFactory.Instance.SpawnParticle(Particles.Prefab, target.transform.position);
        }
        private void OnSkillActivated(GameObject enemy)
        {
            if (Particles.Target == EffectTarget.Enemy && skillActivator.DataPackage.ItemData.Info.ItemInfo is WeaponInfo) return;
            ParticlesFactory.Instance.SpawnParticle(Particles.Prefab, DefineTarget(enemy).transform.position);
        }
        #endregion methods
    }
}