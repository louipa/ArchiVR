using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace Tengio
{
    public class TeleportManager : MonoBehaviour
    {
        [SerializeField] public SceneLoader lobbyLoader;
        [SerializeField] public TeleportationAnchor lobbySpawnPoint;

        public void LoadLobby()
        {
            lobbyLoader.LoadScene(lobbySpawnPoint);
        }   
    }
}
