using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;
using Universal.Events;

namespace Game.UI.Overlay
{
    public abstract class OverlayRequestExecutor<T> : RequestExecutorBehaviour where T : ExecutableRequest
    {
        #region fields & properties
        [SerializeField] private Universal.Behaviour.StateMachineBehaviour overlayStateMachine;
        [SerializeField] private StateChange state;
        #endregion fields & properties

        #region methods
        public override bool TryExecuteRequest(ExecutableRequest request)
        {
            if (request is not T req) return false;
            overlayStateMachine.ApplyState(state);
            ExecuteRequest(req);
            request.Close();
            return true;
        }
        protected abstract void ExecuteRequest(T req);
        #endregion methods
    }
}