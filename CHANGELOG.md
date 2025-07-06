## [1.0.0] - 2025-07-06

### Added
- Exploder: Added component for managing explosion effects. Features include applying explosion force to pieces with physics-based and manual piece finding support, and refreshing pieces after explosion.
- Countdown: Added class for handling in-game countdown functionality with pause support and countdown value modification.
- ContactListener: Added base class and extensions (ContactListener2D and ContactListener3D) for handling both 2D and 3D collision and trigger events.
- Follower: Added component for the following target transform position and rotation. Includes support for world and local space following, lerping options, and snap placement.
- Rotator: Added component for rotating objects around a specified axis with support for rotation in the world or local space.
- SlowMotion: Added class for slowing down in-game timescale and returning to normal speed.
- Helper enums: Added PiecesFindingTypes, RefreshTypes, FollowTypes, LerpTypes, ContactTypes, and ContactStatusTypes enums.
- Comprehensive test coverage:
  - EditMode tests for verifying Countdown functionality's pause and time modification features.
  - PlayMode tests for runtime behavior validation of Countdown, Follower, Rotator, and SlowMotion components.
  - Test framework with both PlayMode and EditMode testing assemblies.

### Changed
- N/A

### Fixed
- N/A

### Removed
- N/A