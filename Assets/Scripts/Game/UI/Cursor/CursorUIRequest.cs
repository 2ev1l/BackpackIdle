using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Events;

namespace Game.UI.Cursor
{
    [System.Serializable]
    public class CursorUIRequest : ExecutableRequest
    {
        #region fields & properties
        private Action OnExecuted;
        public bool DoClickSound => doClickSound;
        [SerializeField] private bool doClickSound = false;
        #endregion fields & properties

        #region methods
        public override void Close()
        {
            OnExecuted?.Invoke();
        }
        public CursorUIRequest(bool doClickSound, Action onExecuted)
        {
            this.doClickSound = doClickSound;
            OnExecuted = onExecuted;
        }
        #endregion methods
    }
}