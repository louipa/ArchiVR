using System;
using Tengio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public InputActionReference menuButton;

    private bool isOpen = false;

    [SerializeField] public SceneLoader sceneLoader;
    [SerializeField] public Camera camera;
    [SerializeField] public TeleportationAnchor lobbySpawnPoint;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isOpen)
        {
            Vector3 vHeadPos = camera.transform.position;
            Vector3 vGazeDir = camera.transform.forward;
            menuCanvas.transform.position = (vHeadPos + vGazeDir * 0.5f) + new Vector3(0.0f, 0.1f, 0.0f);
            Vector3 vRot = camera.transform.eulerAngles; vRot.z = 0;
            menuCanvas.transform.eulerAngles = vRot;
        }
    }

    private void OnEnable()
    {
        menuButton.action.Enable();
        menuButton.action.performed += OnMenuButtonPressed;
    }

    private void OnDisable()
    {
        menuButton.action.performed -= OnMenuButtonPressed;
        menuButton.action.Disable();
    }
    
    private void OnMenuButtonPressed(InputAction.CallbackContext context)
    {
        ToggleMenu();
    }
    

    public void ToggleMenu()
    {
        isOpen = !isOpen;
        menuCanvas.SetActive(isOpen);
    }

    public void LobbyButton()
    {
        ToggleMenu();    
        sceneLoader.LoadScene(lobbySpawnPoint);
    }
    
    public void BackButton()
    {
        ToggleMenu();
    }
}
