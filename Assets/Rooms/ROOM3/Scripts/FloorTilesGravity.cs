using System.Collections;
using UnityEngine;

public class FloorTilesGravity : MonoBehaviour
{
    [SerializeField] float pushSpeed = 3f;
    [SerializeField] float duration = 10f;

    private bool isActive = false;

    private static bool gravityRunning = false;

    public void setActive(bool value)
    {
        isActive = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!isActive) return;
        if (gravityRunning) return;

        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller == null) return;

        StartCoroutine(GravitySequence(controller));
    }

    private IEnumerator GravitySequence(CharacterController controller)
    {
        gravityRunning = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            controller.Move(pushSpeed * Time.deltaTime * Vector3.up);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gravityRunning = false;
    }
}