using System;

namespace CodeCatGames.HMHelpers.Runtime
{
    public enum PiecesFindingTypes
    {
        Inspector,
        Physic
    }

    public enum RefreshTypes
    {
        NonSmooth,
        Smooth
    }
    
    [Flags]
    public enum FollowTypes
    {
        None = 0,
        Position = 1,
        Rotation = 2,
        Everything = 3,
    }

    public enum LerpTypes
    {
        Lerp,
        NonLerp
    }
    
    public enum ContactTypes
    {
        Trigger,
        Collision
    }

    public enum ContactStatusTypes
    {
        Enter,
        Stay,
        Exit
    }
}