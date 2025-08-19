using Game.DataBase;
using Game.Serialization.World;
using Game.UI.Overlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class PlayerInstance : Entity, ISkillTargetProvider
    {
        #region fields & properties
        [SerializeField] private ItemSkillList skillList;
        public GameObject Activator => gameObject;
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            Initialize();
            base.OnEnable();
        }
        private void Initialize()
        {
            Initialize(GameData.Data.PlayerData.Stats);
            skillList.Initialize(GameData.Data.PlayerData.Inventory, this);
        }
        protected override void OnSubscribe()
        {
            base.OnSubscribe();

        }
        protected override void OnUnSubscribe()
        {
            base.OnUnSubscribe();

        }

        public GameObject FindEnemy(ItemInfo forItem)
        {
            //can be realized target search etc...
            var found = transform.parent.GetComponentInChildren<EnemyInstance>(false);
            return found == null ? null : found.gameObject;
        }
        #endregion methods
    }
}