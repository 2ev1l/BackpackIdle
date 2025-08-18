using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [CreateAssetMenu(fileName = "WeaponInfo", menuName = "ScriptableObjects/WeaponInfo")]
    public class WeaponInfo : ItemInfo
    {
        #region fields & properties
        [SerializeField] private bool originalProjectileTexture = false;
        [SerializeField][DrawIf(nameof(originalProjectileTexture), false)] private Sprite projectile;
        public float ProjectileSpeed => projectileSpeed;
        [SerializeField][Min(0)] private float projectileSpeed = 1f;
        #endregion fields & properties

        #region methods
        public Sprite GetProjectileIcon(int level)
        {
            if (!originalProjectileTexture) return projectile;
            return GetLevelInfo(level).ItemIcon;
        }
        #endregion methods
    }
}