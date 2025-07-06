<img src="icon.jpg" width="512" height="512" alt="HMHelpers Logo">

# HMHelpers
HMHelpers is a helper package designed to streamline common tasks in Unity projects. It offers various helper functions for tasks like handling contact events, managing countdowns, applying explosion effects, following objects, rotating objects, and implementing slow motion. This package aims to make Unity development faster and easier by providing essential tools right out of the box.

## Features
HMHelpers offers the following capabilities:
* Contact Handling: Abstract base class and concrete implementations for managing 2D and 3D collision and trigger events with tag filtering and callbacks.
* Countdown Timer: A flexible countdown timer class with pause, resume, and value manipulation functionalities, including optional unscaled time updates and end callbacks.
* Explosion Effects: A component to apply explosion forces to nearby rigidbodies and optionally refresh them to their original positions with smooth or instant transitions.
* Object Following: A component to make a GameObject smoothly or instantly follow the position and/or rotation of a target Transform in the world or local space.
* Object Rotation: A component to rotate a GameObject around a specified axis with a defined speed in the world or local space.
* Slow Motion: A class to easily activate and deactivate slow-motion effects by adjusting the timescale.

## Getting Started
Install via UPM with git URL

`https://github.com/Emre-Emiroglu/HMHelpers.git`

Clone the repository
```bash
git clone https://github.com/Emre-Emiroglu/HMHelpers.git
```
This project is developed using Unity version 6000.0.42f1.

## Usage
* Contact Handling:
    * Add either the `ContactListener2D` or `ContactListener3D` script to a GameObject with a corresponding `Rigidbody2D` or `Rigidbody` component.
    * In the Inspector, configure the Contact Listener Settings:
      * Set the Contact Type to either `Collision` or `Trigger`.
      * Add the tags of the GameObjects you want to interact with to the Contactable Tags array.
    * Access the `EnterCallBack`, `StayCallBack`, and `ExitCallBack` actions from your other scripts to subscribe your custom methods that will be executed upon contact events. These callbacks provide the relevant `Collision`, `Collision2D`, `Collider`, and `Collider2D` components.
        ```csharp
        contactListener.EnterCallBack += OnContactEnter;
        contactListener.ExitCallBack += OnContactExit;
        
        void OnContactEnter(Collision collision, Collision2D collision2D, Collider collider, Collider2D collider2D)
        {
           Debug.Log("Contact Enter with: " + (collider != null ? collider.gameObject.name : collider2D.gameObject.name));
           // Your custom enter logic here
        }
        void OnContactExit(Collision collision, Collision2D collision2D, Collider collider, Collider2D collider2D)
        {
           Debug.Log("Contact Exit with: " + (collider != null ? collider.gameObject.name : collider2D.gameObject.name));
           // Your custom exit logic here
        }
        ```

* Countdown Timer:
    * Create an instance of the `Countdown` class in your script, providing the initial duration.
    * Call the `ExternalUpdate()` method in your `Update()` loop to decrement the timer. You can optionally pass an `Action` to be executed when the countdown finishes.
        ```csharp
        private Countdown timer;
        public float initialTime = 5f;
        
        void Start()
        {
           timer = new Countdown(initialTime);
           timer.ExternalUpdate += OnTimerEnd;
        }
      
        void Update()
        {
           timer.ExternalUpdate();
           Debug.Log("Timer: " + timer.CountDownInternal);
        }

        void OnTimerEnd()
        {
           Debug.Log("Timer finished!");
           // Your timer end logic here
        }
        ```

* Explosion Effects:
    * Add the `Exploder` script to a GameObject that will initiate the explosion.
    * Configure the explosion parameters (radius, force, etc.) and refresh settings in the Inspector.
    * Call the `Explode()` method to trigger the explosion and `Refresh()` to reset the affected pieces.
        ```csharp
        public Exploder exploder;
        
        void Update()
        {
           if (Input.GetKeyDown(KeyCode.Space))
           {
              exploder.Explode();
           }
           if (Input.GetKeyDown(KeyCode.R))
           {
              exploder.Refresh();
           }
        }
        ```

* Object Following:
    * Add the `Follower` script to the GameObject you want to follow another.
    * In the Inspector, assign the Follower Transform and the Target Transform.
    * Configure the following type (position, rotation, or both), space types (world or local), and lerp settings (smooth or instant, and speed).
    * Enable the Can Follow checkbox to start the following behavior.

* Object Rotation:
    * Add the `Rotator` script to the GameObject you want to rotate.
    * In the Inspector, set the Space (world or local), Axis of rotation, and Speed.
    * Enable the Can Rotate checkbox to start the rotation.

* Slow Motion:
    * Create an instance of the SlowMotion class.
    * Use the `Activate()` method to enable slow motion and `DeActivate()` to return to normal time. You can adjust the slow-motion factor using `ChangeFactor()`.
        ```csharp
        private SlowMotion slowMotion = new SlowMotion();
        public float slowMotionFactor = 0.25f;
        
        void Update()
        {
           if (Input.GetKeyDown(KeyCode.S))
           {
              slowMotion.ChangeFactor(slowMotionFactor, true);
              slowMotion.Activate();
           }
           if (Input.GetKeyUp(KeyCode.S))
           {
              slowMotion.DeActivate();
           }
        }
        ```

## Acknowledgments
Special thanks to the Unity community for their invaluable resources and tools.

For more information, visit the GitHub repository.
