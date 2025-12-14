using UnityEngine;

public class Push_Gravity_Field : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    Rigidbody playerBody;
    public float influenceRange;
    public float intensity;
    float distanceToPlayer;
    Vector3 pullForce;
    
    void Start()
    {
        playerBody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyPushForce();
    }

    void ApplyPushForce()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;
        if (distance > influenceRange) return;
        playerBody.AddForce(-direction.normalized * intensity / distance);
    }
}
