# ArchiVR

ArchiVR is a Unity-based virtual reality project for architectural experimentation, designed for the Meta Quest 2. This project provides a template for exploring architectural concepts through a series of explorable rooms.

## Requirements

*   **Unity Version:** `6000.2.6f2`
*   **Unity Packages:**
    *   XR Interaction Toolkit
    *   XR Plugin Management
    *   Oculus XR Plugin (or OpenXR Plugin)
*   **Hardware:** Meta Quest 2 or other compatible VR headset
*   **Software:** Unity Hub, Android SDK for building to Quest

## Getting Started

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    ```
2.  **Open the project in Unity Hub:**
    *   Open Unity Hub.
    *   Click "Open" or "Add project from disk".
    *   Navigate to the cloned project's root folder and select it.
    *   The project should now be listed in Unity Hub. Click on it to open it in the Unity Editor.

## Running in the Editor

To test the project within the Unity Editor, you can use the Play mode with a VR headset connected to your computer.

1.  Make sure your VR headset is connected and recognized by your computer (e.g., via Oculus Link for Quest 2).
2.  Open one of the scenes from the `Assets/Scenes` folder.
3.  Press the "Play" button at the top of the Unity Editor.

## Building for Meta Quest 2

To build and run the project on a Meta Quest 2 device:

1.  **Switch Build Platform:**
    *   Go to `File > Build Settings`.
    *   Select "Android" from the platform list.
    *   Click "Switch Platform".
2.  **Configure Player Settings:**
    *   In the `Build Settings` window, click "Player Settings".
    *   Go to `XR Plug-in Management` and ensure "Oculus" (or "OpenXR" with the appropriate feature group) is checked for the Android tab.
    *   Under `Other Settings`, configure the following:
        *   **Graphics APIs:** Remove "Vulkan" if present, leaving only "OpenGLES3".
        *   **Minimum API Level:** Set to Android 7.0 'Nougat' (API level 24) or higher.
        *   **Scripting Backend:** IL2CPP
        *   **Target Architectures:** ARM64
3.  **Build and Run:**
    *   Connect your Meta Quest 2 to your computer via USB.
    *   Ensure Developer Mode is enabled on your Quest 2.
    *   In the `Build Settings` window, select your device from the "Run Device" dropdown.
    *   Click "Build and Run".
    *   Choose a location to save the build files. Unity will build the project, install the APK on your Quest 2, and launch it.

## Project Structure

*   `Assets/Scenes`: Contains the main scenes for the different architectural rooms.
*   `Assets/Code`: Contains the core C# scripts for interaction and gameplay.
*   `Assets/Rooms`: Contains assets and scripts specific to each room.
*   `ProjectSettings`: Contains the Unity project settings, including version and package information.

## Development

The main logic for player interaction and movement can be found in the scripts within the `Assets/Code` directory. When developing, you can create new scripts and attach them to GameObjects in the scenes to add new functionality.
