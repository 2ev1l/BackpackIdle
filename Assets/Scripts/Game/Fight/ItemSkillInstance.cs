using Game.Serialization.World;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Fight
{
    [RequireComponent(typeof(ItemSkillActivator))]
    public class ItemSkillInstance : ContextActionsItem<SkillDataPackage>
    {
        #region fields & properties
        [SerializeField] private Image icon;
        [SerializeField] private ItemSkillActivator skillActivator;
        #endregion fields & properties

        #region methods
        protected override void UpdateUI()
        {
            base.UpdateUI();
            icon.sprite = Context.ItemData.Info.ItemInfo.GetLevelInfo(Context.ItemData.Level).ItemIcon;
        }
        public override void OnListUpdate(SkillDataPackage param)
        {
            base.OnListUpdate(param);
            skillActivator.Initialize(Context);
            if (Context.ItemData.Info.ItemInfo.IsAutoActivatable)
                skillActivator.ActivateSkillDelayed(0.5f);
        }
        #endregion methods
    }
}