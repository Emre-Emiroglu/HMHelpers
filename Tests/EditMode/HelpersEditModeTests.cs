using CodeCatGames.HMHelpers.Runtime;
using NUnit.Framework;

namespace CodeCatGames.HMHelpers.Tests.EditMode
{
    public sealed class HelpersEditModeTests
    {
        [Test]
        public void ChangeCountdown_SetsNewTime_WhenCalled()
        {
            Countdown countdown = new Countdown(10f);
            
            countdown.ChangeCountdown(5, true);
            
            Assert.AreEqual(5f, countdown.CountDownInternal);
        }

        [Test]
        public void SetPause_SetsPauseState_WhenCalled()
        {
            Countdown countdown = new Countdown(10f);
            
            countdown.SetPause(true);
            
            Assert.IsTrue(countdown.IsPause);
        }
    }
}