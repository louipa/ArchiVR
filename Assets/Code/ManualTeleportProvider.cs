using Unity.XR.CoreUtils;
using UnityEngine;

public class ManualRotationProvider : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin;
    public Transform anchorTransform;

    /// <summary>
    /// Call this from the Teleportation Anchor's Event list
    /// </summary>
    /// <param name="anchorTransform">The Transform of the destination anchor</param>
    public void ForceRotation()
    {
        // rotate the player toward a direction
        xrOrigin.MatchOriginUpCameraForward(anchorTransform.up, anchorTransform.forward);
    }
}
