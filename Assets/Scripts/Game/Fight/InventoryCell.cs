using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Fight
{
    public class InventoryCell : ContextActionsItem<int>
    {
        #region fields & properties
        [SerializeField] private Image icon;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color occupiedColor;
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            SetDefaultUI();
        }
        protected override void UpdateUI()
        {
            base.UpdateUI();
            SetDefaultUI();
        }
        public void SetOccupiedUI()
        {
            icon.color = occupiedColor;
        }
        public void SetDefaultUI()
        {
            icon.color = defaultColor;
        }
        public void SetActiveUI()
        {
            icon.color = activeColor;
        }
        #endregion methods
    }
}