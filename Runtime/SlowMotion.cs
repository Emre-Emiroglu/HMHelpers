using UnityEngine;

namespace CodeCatGames.HMHelpers.Runtime
{
    /// <summary>
    /// A class that handles slow motion functionality by adjusting the timescale and fixed delta time.
    /// </summary>
    public sealed class SlowMotion
    {
        #region Constants
        private const float TimeStep = .02f;
        private const float TimeScale = 1f;
        #endregion

        #region Fields
        private float _factor = .25f;
        #endregion

        #region Executes
        /// <summary>
        /// Changes the slow motion factor. If isSet is true, sets the factor directly; otherwise, adds to the current factor.
        /// </summary>
        /// <param name="factor">The amount to change the factor by.</param>
        /// <param name="isSet">If true, sets the factor to the specified value. Otherwise, adds the factor to the current one.</param>
        public void ChangeFactor(float factor, bool isSet = false) => _factor = isSet ? factor : _factor + factor;
        
        /// <summary>
        /// Activates the slow motion effect by adjusting timescale and fixed delta time.
        /// </summary>
        public void Activate()
        {
            Time.timeScale = _factor;
            Time.fixedDeltaTime = _factor * TimeStep;
        }
        
        /// <summary>
        /// Deactivates the slow motion effect and restores timescale and fixed delta time to normal.
        /// </summary>
        public void DeActivate()
        {
            Time.timeScale = TimeScale;
            Time.fixedDeltaTime = TimeStep;
        }
        #endregion
    }
}