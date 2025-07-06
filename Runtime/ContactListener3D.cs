using UnityEngine;

namespace HMHelpers.Runtime
{
    /// <summary>
    /// A class for handling 3D contact events (collisions or triggers).
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class ContactListener3D : ContactListener
    {
        #region Triggers
        private void OnTriggerEnter(Collider contactCollider)
        {
            if (contactType == ContactTypes.Trigger)
                ContactStatus(ContactStatusTypes.Enter, contactCollider.gameObject.tag, null, null, contactCollider);
        }
        private void OnTriggerStay(Collider contactCollider)
        {
            if (contactType == ContactTypes.Trigger)
                ContactStatus(ContactStatusTypes.Stay, contactCollider.gameObject.tag, null, null, contactCollider);
        }
        private void OnTriggerExit(Collider contactCollider)
        {
            if (contactType == ContactTypes.Trigger)
                ContactStatus(ContactStatusTypes.Exit, contactCollider.gameObject.tag, null, null, contactCollider);
        }
        #endregion

        #region Collisions
        private void OnCollisionEnter(Collision contactCollision)
        {
            if (contactType == ContactTypes.Collision)
                ContactStatus(ContactStatusTypes.Enter, contactCollision.gameObject.tag, contactCollision);
        }
        private void OnCollisionStay(Collision contactCollision)
        {
            if (contactType == ContactTypes.Collision)
                ContactStatus(ContactStatusTypes.Stay, contactCollision.gameObject.tag, contactCollision);
        }
        private void OnCollisionExit(Collision contactCollision)
        {
            if (contactType == ContactTypes.Collision)
                ContactStatus(ContactStatusTypes.Exit, contactCollision.gameObject.tag, contactCollision);
        }
        #endregion
    }
}