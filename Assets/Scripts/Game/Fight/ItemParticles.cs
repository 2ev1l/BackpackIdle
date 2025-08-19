using Game.UI.Overlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;

namespace Game.Fight
{
    public class ItemParticles : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private ItemInstance item;
        [SerializeField] private DestroyablePoolableObject mergeParticles;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            item.OnItemMerged += OnItemMerged;
        }
        private void OnDisable()
        {
            item.OnItemMerged -= OnItemMerged;
        }
        private void OnItemMerged()
        {
            ParticlesFactory.Instance.SpawnParticle(mergeParticles, item.transform.position);
        }
        #endregion methods
    }
}