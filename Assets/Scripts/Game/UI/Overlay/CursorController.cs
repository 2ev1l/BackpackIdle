using UnityEngine;
using UnityEngine.Events;
using Game.Audio;
using AudioType = Game.Audio.AudioType;
using Game.UI.Cursor;
using Universal.Events;
using Zenject;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class CursorController : RequestExecutor
    {
        #region fields & properties
        [SerializeField] private AudioClip onClickSound;

        #endregion fields & properties

        #region methods
        public void DoClickSound()
        {
            if (onClickSound == null) return;
            AudioManager.PlayClip(onClickSound, AudioType.Sound);
        }

        public override bool TryExecuteRequest(ExecutableRequest request)
        {
            if (request is not CursorUIRequest cursorRequest) return false;
            if (cursorRequest.DoClickSound)
            {
                DoClickSound();
                return true;
            }

            cursorRequest.Close();
            return true;
        }
        #endregion methods
    }
}