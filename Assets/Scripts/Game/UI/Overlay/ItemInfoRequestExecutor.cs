using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;
using Universal.Events;

namespace Game.UI.Overlay
{
    public class ItemInfoRequestExecutor : RequestExecutorBehaviour
    {
        #region fields & properties
        [SerializeField] private Universal.Behaviour.StateMachineBehaviour overlayStateMachine;
        [SerializeField] private StateChange infoState;
        [SerializeField] private ItemInfoExposer infoExposer;
        #endregion fields & properties

        #region methods
        public override bool TryExecuteRequest(ExecutableRequest request)
        {
            if (request is not ItemInfoRequest info) return false;
            overlayStateMachine.ApplyState(infoState);
            infoExposer.Expose(info.ItemInfo, info.Level);
            request.Close();
            return true;
        }
        #endregion methods
    }
}