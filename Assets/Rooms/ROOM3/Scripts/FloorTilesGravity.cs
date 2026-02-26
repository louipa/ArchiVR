using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class FloorTilesGravity : MonoBehaviour
{
    [Header("Propulsion Settings")]
    [SerializeField] float pushSpeed = 3f;
    [SerializeField] float duration = 2f; // durée de la montée

    [Header("Vignette Settings")]
    [SerializeField] TunnelingVignetteController vignetteController;
    [SerializeField] LocomotionVignetteProvider vignetteProvider;

    [Header("Safety Settings")]
    [SerializeField] float maxFallTime = 5f; // timeout pour désactiver la vignette

    private bool isActive = false;
    private static bool gravityRunning = false;

    public void setActive(bool value) => isActive = value;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !isActive || gravityRunning)
            return;

        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller == null) return;

        StartCoroutine(GravitySequence(controller));
    }

    private IEnumerator GravitySequence(CharacterController controller)
    {
        gravityRunning = true;

        // activer la vignette
        vignetteController.BeginTunnelingVignette(vignetteProvider);

        // propulsion vers le haut
        float elapsed = 0f;
        while (elapsed < duration)
        {
            controller.Move(pushSpeed * Time.deltaTime * Vector3.up);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // attendre "la retombée" avec timeout
        float timer = 0f;
        while (timer < maxFallTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // désactiver la vignette
        vignetteController.EndTunnelingVignette(vignetteProvider);

        gravityRunning = false;
    }
}