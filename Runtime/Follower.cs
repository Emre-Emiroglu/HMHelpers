using UnityEngine;

namespace CodeCatGames.HMHelpers.Runtime
{
    public sealed class Follower : MonoBehaviour
    {
        #region Fields
        [Header("Follower Settings")]
        [SerializeField] private Transform follower;
        [SerializeField] private FollowTypes followType;
        [SerializeField] private Space positionSpaceType;
        [SerializeField] private Space rotationSpaceType;
        [SerializeField] private LerpTypes positionLerpType;
        [SerializeField] private LerpTypes rotationLerpType;
        [SerializeField] private bool canFollow;
        [SerializeField] private bool withSnapBegin;
        [Header("Target Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private Space targetPositionSpaceType;
        [SerializeField] private Space targetRotationSpaceType;
        [Header("Speed Settings")]
        [Range(0f, 100)][SerializeField] private float positionLerpSpeed = .25f;
        [Range(0f, 100)][SerializeField] private float rotationLerpSpeed = .25f;
        #endregion
        
        #region Core
        private void Awake()
        {
            if (!withSnapBegin)
                return;
            
            SetupSnap();
        }
        #endregion

        #region Executes
        private void SetupSnap()
        {
            if (followType.HasFlag(FollowTypes.Position))
            {
                switch (positionSpaceType)
                {
                    case Space.World:
                        follower.position = GetTargetPositionAndRotation().Item1;
                        break;
                    case Space.Self:
                        follower.localPosition = GetTargetPositionAndRotation().Item1;
                        break;
                }
            }

            if (followType.HasFlag(FollowTypes.Rotation))
            {
                switch (rotationSpaceType)
                {
                    case Space.World:
                        follower.rotation = GetTargetPositionAndRotation().Item2;
                        break;
                    case Space.Self:
                        follower.localRotation = GetTargetPositionAndRotation().Item2;
                        break;
                }
            }
        }
        private void FollowLogic()
        {
            Vector3 targetPos = GetTargetPositionAndRotation().Item1;
            Quaternion targetRot = GetTargetPositionAndRotation().Item2;

            if (followType.HasFlag(FollowTypes.Position))
            {
                switch (positionSpaceType)
                {
                    case Space.World:
                        switch (positionLerpType)
                        {
                            case LerpTypes.Lerp:
                                follower.position = Vector3.Lerp(follower.position, targetPos,
                                    Time.deltaTime * positionLerpSpeed);
                                break;
                            case LerpTypes.NonLerp:
                                follower.position = targetPos;
                                break;
                        }
                        break;
                    case Space.Self:
                        switch (positionLerpType)
                        {
                            case LerpTypes.Lerp:
                                follower.localPosition = Vector3.Lerp(follower.localPosition, targetPos,
                                    Time.deltaTime * positionLerpSpeed);
                                break;
                            case LerpTypes.NonLerp:
                                follower.localPosition = targetPos;
                                break;
                        }
                        break;
                }
            }

            if (followType.HasFlag(FollowTypes.Rotation))
            {
                switch (rotationSpaceType)
                {
                    case Space.World:
                        switch (rotationLerpType)
                        {
                            case LerpTypes.Lerp:
                                follower.rotation = Quaternion.Lerp(follower.rotation, targetRot,
                                    Time.deltaTime * rotationLerpSpeed);
                                break;
                            case LerpTypes.NonLerp:
                                follower.rotation = targetRot;
                                break;
                        }
                        break;
                    case Space.Self:
                        switch (rotationLerpType)
                        {
                            case LerpTypes.Lerp:
                                follower.localRotation = Quaternion.Lerp(follower.localRotation, targetRot,
                                    Time.deltaTime * rotationLerpSpeed);
                                break;
                            case LerpTypes.NonLerp:
                                follower.localRotation = targetRot;
                                break;
                        }
                        break;
                }
            }
        }
        private (Vector3, Quaternion) GetTargetPositionAndRotation()
        {
            Vector3 pos = new();
            Quaternion rot = Quaternion.identity;

            switch (targetPositionSpaceType)
            {
                case Space.World:
                    pos = target.position;
                    break;
                case Space.Self:
                    pos = target.localPosition;
                    break;
            }

            switch (targetRotationSpaceType)
            {
                case Space.World:
                    rot = target.rotation;
                    break;
                case Space.Self:
                    rot = target.localRotation;
                    break;
            }

            return (pos, rot);
        }
        public void SetCanFollow(bool newFollowStatus) => canFollow = newFollowStatus;
        #endregion

        #region Updates
        private void Update()
        {
            if (!canFollow)
                return;

            FollowLogic();
        }
        #endregion
    }
}