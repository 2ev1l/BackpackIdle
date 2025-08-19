using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Overlay
{
    public class ItemInfoExposer : MonoBehaviour
    {
        #region fields & properties
        public const string LEVEL_PREFIX = "Lv.";

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image icon;
        [SerializeField] private ItemEffectList effectList;
        #endregion fields & properties

        #region methods
        public void Expose(ItemInfo info, int level)
        {
            nameText.text = info.Name;
            levelText.text = $"{LEVEL_PREFIX}{level}";
            icon.sprite = info.GetLevelInfo(level).ItemIcon;
            effectList.Initialize(info, level);
        }
        #endregion methods
    }
}