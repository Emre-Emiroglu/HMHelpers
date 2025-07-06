using System;
using UnityEngine;

namespace HMHelpers.Runtime
{
    /// <summary>
    /// Abstract base class for handling contact events (collisions or triggers) in both 2D and 3D contexts.
    /// </summary>
    public abstract class ContactListener : MonoBehaviour
    {
        #region Actions
        /// <summary>
        /// Enter callback action for different enter contact events.
        /// </summary>
        public Action<Collision, Collision2D, Collider, Collider2D> EnterCallBack;
        
        /// <summary>
        /// Stay callback action for different stay contact events.
        /// </summary>
        public Action<Collision, Collision2D, Collider, Collider2D> StayCallBack;
        
        /// <summary>
        /// Exit callback action for different exit contact events.
        /// </summary>
        public Action<Collision, Collision2D, Collider, Collider2D> ExitCallBack;
        #endregion
        
        #region Fields
        [Header("Contact Listener Settings")]
        [SerializeField] protected ContactTypes contactType;
        [SerializeField] protected string[] contactableTags;
        #endregion

        #region Executes
        /// <summary>
        /// Checks and invokes the correct callback for the contact status (Enter, Stay, Exit).
        /// </summary>
        /// <param name="contactStatusType">Type of contact (Enter, Stay, Exit)</param>
        /// <param name="tagName">Tag of the object involved in the contact</param>
        /// <param name="contactCollision">Collision event (3D)</param>
        /// <param name="contactCollision2D">Collision event (2D)</param>
        /// <param name="contactCollider">Collider event (3D)</param>
        /// <param name="contactCollider2D">Collider event (2D)</param>
        public void ContactStatus(ContactStatusTypes contactStatusType, string tagName,
            Collision contactCollision = null, Collision2D contactCollision2D = null, Collider contactCollider = null,
            Collider2D contactCollider2D = null)
        {
            bool isContain = CompareCheck(tagName);
            if (isContain)
            {
                switch (contactStatusType)
                {
                    case ContactStatusTypes.Enter:
                        EnterCallBack?.Invoke(contactCollision, contactCollision2D, contactCollider, contactCollider2D);
                        break;
                    case ContactStatusTypes.Stay:
                        StayCallBack?.Invoke(contactCollision, contactCollision2D, contactCollider, contactCollider2D);
                        break;
                    case ContactStatusTypes.Exit:
                        ExitCallBack?.Invoke(contactCollision, contactCollision2D, contactCollider, contactCollider2D);
                        break;
                }
            }
            else
                Debug.Log($"{tag} tag is not in contactableTags");
        }
        private bool CompareCheck(string tagName)
        {
            int tagsCount = contactableTags.Length;
            
            if (tagsCount == 0)
                Debug.LogError("Contactable Tags Cannot Be 0!");
            else
            {
                for (int i = 0; i < tagsCount; i++)
                {
                    bool isEqual = contactableTags[i] == tagName;
                    if (isEqual)
                        return true;
                }
            }

            return false;
        }
        #endregion
    }
}