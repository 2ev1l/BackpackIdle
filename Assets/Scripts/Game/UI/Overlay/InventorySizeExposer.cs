using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.Overlay
{
    public class InventorySizeExposer : MonoBehaviour
    {
        #region fields & properties
        private static string WidthLanguageText => LanguageInfo.GetTextByType(TextType.Game, 12);
        private static string HeightLanguageText => LanguageInfo.GetTextByType(TextType.Game, 13);

        [SerializeField] private TextMeshProUGUI widthText;
        [SerializeField] private TextMeshProUGUI heightText;
        private InventoryData inventory;
        private Vector2Int maxSize = new(1, 1);
        #endregion fields & properties

        #region methods
        private void OnDisable()
        {
            UnSubscribe();
        }
        public void Expose(InventoryData inventory, Vector2Int maxSize)
        {
            UnSubscribe();
            this.maxSize = maxSize;
            this.inventory = inventory;
            UpdateUI();
            Subscribe();
        }
        private void UpdateUI()
        {
            widthText.text = $"{WidthLanguageText}: {inventory.Width}/{maxSize.x}";
            heightText.text = $"{HeightLanguageText}: {inventory.Height}/{maxSize.y}";
        }
        private void Subscribe()
        {
            if (inventory == null) return;
            inventory.OnSizeChanged += UpdateUI;
        }
        private void UnSubscribe()
        {
            if (inventory == null) return;
            inventory.OnSizeChanged -= UpdateUI;
        }
        #endregion methods
    }
}