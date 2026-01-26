using Tengio;
using UnityEngine;

public class SceneLoadingArea : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private SceneLoader sceneLoader;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            sceneLoader.LoadScene(sceneName);
        }
    }
}
