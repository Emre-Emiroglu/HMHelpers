using System.Collections;
using System.Reflection;
using CodeCatGames.HMHelpers.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CodeCatGames.HMHelpers.Tests.PlayMode
{
    public sealed class HelpersPlayModeTests
    {
        private const float DefaultTimeScale = 1f;
        private const float DefaultFixedDeltaTime = 0.02f;
        
        private Countdown _countdown;
        
        private GameObject _followerObject;
        private GameObject _targetObject;
        private Follower _follower;
        
        private GameObject _rotatorObject;
        private Rotator _rotator;
        
        private SlowMotion _slowMotion;

        [SetUp]
        public void Setup()
        {
            _countdown = new Countdown(5f);
            
            _targetObject = new GameObject("Target")
            {
                transform =
                {
                    position = new Vector3(10, 0, 0),
                    rotation = Quaternion.Euler(0, 90, 0)
                }
            };

            _followerObject = new GameObject("FollowerObject");
            _follower = _followerObject.AddComponent<Follower>();

            SetPrivateField<Follower>("target", _follower, _targetObject.transform);
            SetPrivateField<Follower>("follower", _follower, _followerObject.transform);
            SetPrivateField<Follower>("followType", _follower, FollowTypes.Position | FollowTypes.Rotation);
            SetPrivateField<Follower>("positionSpaceType", _follower, Space.World);
            SetPrivateField<Follower>("rotationSpaceType", _follower, Space.World);
            SetPrivateField<Follower>("targetPositionSpaceType", _follower, Space.World);
            SetPrivateField<Follower>("targetRotationSpaceType", _follower, Space.World);
            SetPrivateField<Follower>("positionLerpType", _follower, LerpTypes.NonLerp);
            SetPrivateField<Follower>("rotationLerpType", _follower, LerpTypes.NonLerp);
            SetPrivateField<Follower>("canFollow", _follower, true);
            
            _rotatorObject = new GameObject("RotatorObject");
            _rotator = _rotatorObject.AddComponent<Rotator>();

            SetPrivateField<Rotator>("axis", _rotator, Vector3.up);
            SetPrivateField<Rotator>("speed", _rotator, 180f);
            SetPrivateField<Rotator>("space", _rotator, Space.World);
            SetPrivateField<Rotator>("canRotate", _rotator, false);
            
            _slowMotion = new SlowMotion();
            Time.timeScale = DefaultTimeScale;
            Time.fixedDeltaTime = DefaultFixedDeltaTime;
            
        }
        
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_targetObject);
            Object.Destroy(_followerObject);
            
            Object.Destroy(_rotatorObject);
            
            Time.timeScale = DefaultTimeScale;
            Time.fixedDeltaTime = DefaultFixedDeltaTime;
        }
        
        [UnityTest]
        public IEnumerator ExternalUpdate_DecreasesCountdown_WhenNotPaused()
        {
            _countdown.ExternalUpdate();
            
            yield return null;
            
            Assert.Less(_countdown.CountDownInternal, 5f);
        }

        [UnityTest]
        public IEnumerator ExternalUpdate_DoesNotDecreaseCountdown_WhenPaused()
        {
            _countdown.SetPause(true);
            
            _countdown.ExternalUpdate();
            
            yield return null;
            
            Assert.AreEqual(5f, _countdown.CountDownInternal);
        }

        [UnityTest]
        public IEnumerator ExternalUpdate_CallsCountdownEnded_WhenTimeIsZero()
        {
            _countdown.ChangeCountdown(-5, true);
            
            bool countdownEndedCalled = false;
            
            _countdown.ExternalUpdate(() => countdownEndedCalled = true);
            
            yield return null;
            
            Assert.IsTrue(countdownEndedCalled);
        }
        
        [UnityTest]
        public IEnumerator Follower_ShouldSnapToTarget_WhenWithSnapBegin()
        {
            yield return null;

            Assert.AreEqual(_targetObject.transform.position, _followerObject.transform.position);
            Assert.AreEqual(_targetObject.transform.rotation.eulerAngles, _followerObject.transform.rotation.eulerAngles);
        }

        [UnityTest]
        public IEnumerator Follower_ShouldLerpToTarget_WhenCanFollowEnabled()
        {
            SetPrivateField<Follower>("positionLerpType", _follower, LerpTypes.Lerp);
            SetPrivateField<Follower>("rotationLerpType", _follower, LerpTypes.Lerp);
            SetPrivateField<Follower>("positionLerpSpeed", _follower, 100f);
            SetPrivateField<Follower>("rotationLerpSpeed", _follower, 100f);

            _followerObject.transform.position = Vector3.zero;
            _followerObject.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(0.1f);

            Assert.AreNotEqual(Vector3.zero, _followerObject.transform.position);
            Assert.AreNotEqual(Quaternion.identity.eulerAngles, _followerObject.transform.rotation.eulerAngles);
        }
        
        [UnityTest]
        public IEnumerator Rotator_DoesNotRotate_WhenDisabled()
        {
            Quaternion initialRotation = _rotatorObject.transform.rotation;
        
            yield return new WaitForSeconds(0.2f);
        
            Assert.AreEqual(initialRotation.eulerAngles, _rotatorObject.transform.rotation.eulerAngles);
        }
        
        [UnityTest]
        public IEnumerator Rotator_Rotates_WhenEnabled()
        {
            _rotator.SetCanRotate(true);
            
            Quaternion initialRotation = _rotatorObject.transform.rotation;
        
            yield return new WaitForSeconds(0.2f);
        
            Assert.AreNotEqual(initialRotation.eulerAngles, _rotatorObject.transform.rotation.eulerAngles);
        }
        
        [UnityTest]
        public IEnumerator SlowMotion_Activate_SetsCorrectTimeScaleAndFixedDeltaTime()
        {
            _slowMotion.Activate();

            yield return null;

            Assert.AreEqual(0.25f, Time.timeScale, 0.001f);
            Assert.AreEqual(0.25f * 0.02f, Time.fixedDeltaTime, 0.001f);
        }

        [UnityTest]
        public IEnumerator SlowMotion_DeActivate_ResetsTimeValues()
        {
            _slowMotion.Activate();
            
            yield return null;

            _slowMotion.DeActivate();
            
            yield return null;

            Assert.AreEqual(DefaultTimeScale, Time.timeScale, 0.001f);
            Assert.AreEqual(DefaultFixedDeltaTime, Time.fixedDeltaTime, 0.001f);
        }

        [UnityTest]
        public IEnumerator SlowMotion_ChangeFactor_WithSetTrue_OverridesFactor()
        {
            _slowMotion.ChangeFactor(0.5f, true);
            
            _slowMotion.Activate();

            yield return null;

            Assert.AreEqual(0.5f, Time.timeScale, 0.001f);
            Assert.AreEqual(0.5f * 0.02f, Time.fixedDeltaTime, 0.001f);
        }

        [UnityTest]
        public IEnumerator SlowMotion_ChangeFactor_WithSetFalse_AddsToFactor()
        {
            _slowMotion.ChangeFactor(0.1f);
            
            _slowMotion.Activate();

            yield return null;

            Assert.AreEqual(0.35f, Time.timeScale, 0.001f);
            Assert.AreEqual(0.35f * 0.02f, Time.fixedDeltaTime, 0.001f);
        }
        
        private void SetPrivateField<TType>(string name, object classField, object value)
        {
            FieldInfo field = typeof(TType).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            field!.SetValue(classField, value);
        }
    }
}