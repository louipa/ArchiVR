using Tengio;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class SceneLoadingArea : MonoBehaviour
{
    [SerializeField] private TeleportationAnchor anchor;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private ManualRotationProvider rotationLoader;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sceneLoader.LoadScene(anchor, null, () => { rotationLoader.ForceRotation();}, null);
        }
    }
}
