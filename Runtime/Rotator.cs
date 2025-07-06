using UnityEngine;

namespace HMHelpers.Runtime
{
    /// <summary>
    /// A class that rotates an object around a specified axis with a given speed in either world or local space.
    /// </summary>
    public sealed class Rotator : MonoBehaviour
    {
        #region Fields
        [Header("Rotator Settings")]
        [SerializeField] private Space space = Space.World;
        [SerializeField] private Vector3 axis = Vector3.right;
        [SerializeField] private bool canRotate;
        [Range(0f, 360f)][SerializeField] private float speed = 180f;
        #endregion

        #region Executes
        private void Rotate() => transform.Rotate(axis, speed * Time.deltaTime, space);
        
        /// <summary>
        /// Sets whether the object can rotate or not.
        /// </summary>
        /// <param name="newCanRotateStatus">New rotation status. If true, the object will rotate.</param>
        public void SetCanRotate(bool newCanRotateStatus) => canRotate = newCanRotateStatus;
        #endregion

        #region Update
        private void Update()
        {
            if (!canRotate)
                return;

            Rotate();
        }
        #endregion
    }
}