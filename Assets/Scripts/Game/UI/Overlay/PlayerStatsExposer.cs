using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class PlayerStatsExposer : EntityStatsExposer
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            Initialize(GameData.Data.PlayerData.Stats);
            base.OnEnable();
        }
        #endregion methods
    }
}