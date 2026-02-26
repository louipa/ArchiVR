using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;
public class ReturnToOrigin : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Record the starting position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Call this function when the grab is finished
    public void ResetPosition()
    {
        StartCoroutine(SmoothReturn());
    }

    IEnumerator SmoothReturn()
    {
        float duration = 2f; // Time in seconds to return
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        if (rb != null) rb.isKinematic = true; // Disable physics during return

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, initialPosition, elapsed / duration);
            transform.rotation = Quaternion.Slerp(startRot, initialRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        if (rb != null) rb.isKinematic = false; 
    }
}
