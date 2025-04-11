using UnityEngine;

namespace CodeCatGames.HMHelpers.Runtime
{
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