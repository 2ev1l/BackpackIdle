using Game.DataBase;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Overlay
{
    public class ItemEffectItem : ContextActionsItem<EffectStatInfo>
    {
        #region fields & properties
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        #endregion fields & properties

        #region methods
        protected override void UpdateUI()
        {
            base.UpdateUI();
            icon.sprite = Context.Icon;
            title.text = Context.Name;
            description.text = Context.GetEffectText();
        }
        #endregion methods
    }
}