using System;
using UnityEngine;

namespace HMHelpers.Runtime
{
    /// <summary>
    /// A countdown timer class that handles countdown logic with optional pause functionality.
    /// </summary>
    public sealed class Countdown
    {
        #region ReadonlyFields
        private float _countdownInternal;
        private bool _pause;
        #endregion

        #region Getters
        /// <summary>
        /// Gets the current countdown value in seconds.
        /// </summary>
        /// <value>
        /// The current countdown value.
        /// </value>
        public float CountDownInternal => _countdownInternal;
        
        /// <summary>
        /// Gets a value indicating whether the countdown is paused.
        /// </summary>
        /// <value>
        /// <c>true</c> if the countdown is paused; otherwise, <c>false</c>.
        /// </value>
        public bool IsPause => _pause;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Countdown"/> class with a specified countdown value.
        /// </summary>
        /// <param name="countdownInternal">The initial countdown value in seconds.</param>
        /// <param name="pause">Indicates whether the countdown should be paused initially. Default is false.</param>
        public Countdown(float countdownInternal, bool pause = false)
        {
            _countdownInternal = countdownInternal;
            _pause = pause;
        }
        #endregion

        #region Execytes
        /// <summary>
        /// Pauses or resumes the countdown.
        /// </summary>
        /// <param name="isPause">If set to <c>true</c>, the countdown will be paused; otherwise, it will continue.</param>
        public void SetPause(bool isPause) => _pause = isPause;
        
        /// <summary>
        /// Changes the countdown value by the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds to change the countdown by (positive or negative).</param>
        /// <param name="isSet">If set to <c>true</c>, the countdown is set to the new value. Otherwise, the countdown is incremented/decremented.</param>
        public void ChangeCountdown(int seconds, bool isSet = false) =>
            _countdownInternal = isSet ? seconds : _countdownInternal + seconds;
        #endregion

        #region Update
        /// <summary>
        /// Updates the countdown and invokes a callback when the countdown reaches zero.
        /// </summary>
        /// <param name="countDownEnded">An optional callback to be invoked when the countdown ends.</param>
        /// <param name="unscaled">If <c>true</c>, the countdown will be updated using unscaled time. Otherwise, it will use the scaled time.</param>
        public void ExternalUpdate(Action countDownEnded = null, bool unscaled = false)
        {
            if (_pause)
                return;

            _countdownInternal -= unscaled ? Time.unscaledDeltaTime : Time.deltaTime;

            if (_countdownInternal >= 0)
                return;
                
            _countdownInternal = 0;
                    
            _pause = true;
            
            countDownEnded?.Invoke();
        }
        #endregion
    }
}