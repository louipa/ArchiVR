using Tengio;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Rooms.ROOM1
{
    public class BookExit : MonoBehaviour
    {
        [SerializeField] public SceneLoader sceneLoader;
        [SerializeField] public TeleportationAnchor lobbySpawnPoint;
        [SerializeField] public XRGrabInteractable book;

        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip bookTakenSound;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private bool animationRunning = false;

        private void Awake()
        {
            initialPosition = book.transform.position;
            initialRotation = book.transform.rotation;
        }

        public void TriggerBookTaken()
        {
            if (animationRunning)
                return;
            
            animationRunning = true;
            // jouer le son immédiatement
            if (audioSource != null && bookTakenSound != null)
            {
                audioSource.PlayOneShot(bookTakenSound);
            }

            StartCoroutine(ExitSequence());
        }

        private IEnumerator ExitSequence()
        {
            yield return new WaitForSeconds(5f);

            XRGrabInteractable grab = book.GetComponent<XRGrabInteractable>();
            if (grab != null && grab.isSelected)
            {
                grab.interactionManager.CancelInteractableSelection((IXRSelectInteractable)book);
            }

            book.transform.position = initialPosition;
            book.transform.rotation = initialRotation;

            Rigidbody rb = book.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            sceneLoader.LoadScene(lobbySpawnPoint);
            animationRunning = false;
        }
    }
}