using Game.UI.Overlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    public class EnemyInstance : Entity
    {
        #region fields & properties
        private EnemyInfo info;
        [SerializeField] private EntityStatsExposer statsExposer;
        #endregion fields & properties

        #region methods
        public void Initialize(EnemyInfo info)
        {
            this.info = info;
            EntityStats stats = info.Stats;
            base.Initialize(stats);
            statsExposer.Initialize(stats);
        }
        #endregion methods
    }
}