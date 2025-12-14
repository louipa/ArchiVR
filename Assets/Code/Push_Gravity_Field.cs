using Unity.XR.CoreUtils;
using UnityEngine;

public class Push_Gravity_Field : MonoBehaviour
{
    public XROrigin xrOrigin;
    public float influenceRange = 5f;
    public float intensity = 10f;
    Rigidbody playerBody;
    
    void Update()
    {
        ApplyPushForce();
    }

    void ApplyPushForce()
    {
        Vector3 playerPosition = new Vector3(
            xrOrigin.Camera.transform.position.x,
            xrOrigin.transform.position.y,
            xrOrigin.Camera.transform.position.z
        );

        Vector3 direction = playerPosition - transform.position;
        float distance = direction.magnitude;

        if (distance > influenceRange)
            return;

        Vector3 displacement =
            direction.normalized * intensity * Time.deltaTime;

        xrOrigin.transform.position += displacement;
    }
}
