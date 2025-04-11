using System;
using UnityEngine;

namespace CodeCatGames.HMHelpers.Runtime
{
    public sealed class Countdown
    {
        #region ReadonlyFields
        private float _countdownInternal;
        private bool _pause;
        #endregion

        #region Getters
        public double CountDownInternal => _countdownInternal;
        public bool IsPause => _pause;
        #endregion

        #region Constructor
        public Countdown(float countdownInternal, bool pause = false)
        {
            _countdownInternal = countdownInternal;
            _pause = pause;
        }
        #endregion

        #region Execytes
        public void SetPause(bool isPause) => _pause = isPause;
        public void ChangeCountdown(int seconds, bool isSet = false) =>
            _countdownInternal = isSet ? seconds : _countdownInternal + seconds;
        #endregion

        #region Update
        public void ExternalUpdate(Action countDownEnded = null, bool unscaled = false)
        {
            if (_pause)
                return;

            if (_countdownInternal <= 0)
                return;
            
            _countdownInternal -= unscaled ? Time.unscaledDeltaTime : Time.deltaTime;

            if (_countdownInternal >= 0)
                return;
                
            _countdownInternal = 0;
                    
            countDownEnded?.Invoke();
        }
        #endregion
    }
}