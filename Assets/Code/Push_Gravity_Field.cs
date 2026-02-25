using Unity.XR.CoreUtils;
using UnityEngine;

public class Push_Gravity_Field : MonoBehaviour
{
    public XROrigin xrOrigin;
    public float influenceRange = 5f;
    public float intensity = 10f;
    
    void Update()
    {
        ApplyPushForce();
    }

    void ApplyPushForce()
    {
        Vector2 playerPosition = new Vector2(
            xrOrigin.Camera.transform.position.x,
            xrOrigin.Camera.transform.position.z
        );
        Vector2 platformPosition = new Vector2(transform.position.x, transform.position.z);

        Vector2 direction = playerPosition - platformPosition;
        float distance = direction.magnitude;

        if (distance > influenceRange)
            return;

        Vector2 displacement = direction.normalized * intensity * Time.deltaTime;

        Vector3 originPos = xrOrigin.transform.position;
        originPos.x += displacement.x;
        originPos.z += displacement.y;
        xrOrigin.transform.position = originPos;
    }
}
