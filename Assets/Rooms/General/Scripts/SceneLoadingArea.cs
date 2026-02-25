using Tengio;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class SceneLoadingArea : MonoBehaviour
{
    [SerializeField] private TeleportationAnchor anchor;
    [SerializeField] private SceneLoader sceneLoader;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.tag == "Player")
        {
            sceneLoader.LoadScene(anchor);
        }
    }
}
