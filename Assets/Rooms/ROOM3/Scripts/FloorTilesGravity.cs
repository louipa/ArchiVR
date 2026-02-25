using System.Collections;
using UnityEngine;

public class FloorTilesGravity : MonoBehaviour
{
    [SerializeField] float reverseGravity = 5f;
    [SerializeField] float normalGravity = -9.81f;
    [SerializeField] float transitionSpeed = 2f;
    [SerializeField] float duration = 10f;

    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (isActive) return;

        isActive = true;
        StartCoroutine(GravitySequence());
    }

    private IEnumerator GravitySequence()
    {
        // transition vers gravité inversée
        yield return StartCoroutine(ChangeGravity(reverseGravity));

        // attendre 10 secondes
        yield return new WaitForSeconds(duration);

        // retour gravité normale
        yield return StartCoroutine(ChangeGravity(normalGravity));

        isActive = false;
    }

    private IEnumerator ChangeGravity(float targetGravity)
    {
        float current = Physics.gravity.y;

        while (Mathf.Abs(current - targetGravity) > 0.05f)
        {
            current = Mathf.Lerp(current, targetGravity, Time.deltaTime * transitionSpeed);
            Physics.gravity = new Vector3(0, current, 0);
            yield return null;
        }

        Physics.gravity = new Vector3(0, targetGravity, 0);
    }
}