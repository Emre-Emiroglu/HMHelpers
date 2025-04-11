using UnityEngine;

namespace CodeCatGames.HMHelpers.Runtime
{
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
        public void ChangeFactor(float factor, bool isSet = false) => _factor = isSet ? factor : _factor + factor;
        public void Activate()
        {
            Time.timeScale = _factor;
            Time.fixedDeltaTime = _factor * TimeStep;
        }
        public void DeActivate()
        {
            Time.timeScale = TimeScale;
            Time.fixedDeltaTime = TimeStep;
        }
        #endregion
    }
}