
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CubeTool : MonoBehaviour
{
    [Header("Hand Transforms")]
    public Transform leftHand;
    public Transform rightHand;
	public Camera mainCamera;

    [Header("Cube Prefab & Materials")]
    public GameObject cubePrefab; // Final cube prefab (e.g., Unity Cube with collider)
    public Material previewMaterial; // Transparent material for preview

    [Header("Settings")]
    public float minSize = 0.02f; // Minimum edge size to create a cube

    private GameObject previewObject;
    private MeshRenderer previewRenderer;
    private bool leftTriggerHeld;
    private bool rightTriggerHeld;
	private bool building; // are we in building mode (after second trigger press)
	private bool hasAnchor; // has first trigger been pressed (anchor captured)
	private Transform anchorHand;
	private Transform secondHand;
	private Vector3 anchorPoint;

    private readonly List<InputDevice> leftDevices = new List<InputDevice>();
    private readonly List<InputDevice> rightDevices = new List<InputDevice>();

    void Start()
    {
        TryGetDevices();
        CreatePreviewObject();
    }

    void TryGetDevices()
    {
        leftDevices.Clear();
        rightDevices.Clear();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightDevices);
    }

    void CreatePreviewObject()
    {
        previewObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        previewObject.name = "CubePreview";
        previewRenderer = previewObject.GetComponent<MeshRenderer>();
        if (previewMaterial != null)
        {
            previewRenderer.sharedMaterial = previewMaterial;
        }
        // Disable collisions on preview
        var col = previewObject.GetComponent<Collider>();
        if (col) col.enabled = false;
        previewObject.SetActive(false);
    }

    void Update()
    {
        if (leftHand == null || rightHand == null)
        {
            return;
        }

        if (leftDevices.Count == 0 || rightDevices.Count == 0)
        {
            TryGetDevices();
        }

        bool leftPressed = ReadTrigger(leftDevices);
        bool rightPressed = ReadTrigger(rightDevices);


		bool leftDown = leftPressed && !leftTriggerHeld;
		bool rightDown = rightPressed && !rightTriggerHeld;
		bool leftUp = !leftPressed && leftTriggerHeld;
		bool rightUp = !rightPressed && rightTriggerHeld;

		if (!hasAnchor)
		{
			if (leftDown)
			{
				hasAnchor = true;
				anchorHand = leftHand;
				anchorPoint = leftHand.position;
			}
			else if (rightDown)
			{
				hasAnchor = true;
				anchorHand = rightHand;
				anchorPoint = rightHand.position;
			}
		}

		if (!building && hasAnchor)
		{
			bool anchorIsLeft = anchorHand == leftHand;
			if (anchorIsLeft && rightDown)
			{
				building = true;
				secondHand = rightHand;
			}
			else if (!anchorIsLeft && leftDown)
			{
				building = true;
				secondHand = leftHand;
			}
		}

		if (building)
		{
			if (!previewObject.activeSelf)
            {
                previewObject.SetActive(true);
            }

			UpdateCube();
			if (leftUp || rightUp)
			{
				FinalizeCube();
				building = false;
				hasAnchor = false;
				secondHand = null;
			}

		}  else if (previewObject.activeSelf) {
            previewObject.SetActive(false);
        }

		leftTriggerHeld = leftPressed;
		rightTriggerHeld = rightPressed;
    }

    bool ReadTrigger(List<InputDevice> devices)
    {
        for (int i = 0; i < devices.Count; i++)
        {
            var device = devices[i];
            if (!device.isValid) continue;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool pressed))
            {
                if (pressed) return true;
            }
        }
        return false;
    }

void UpdateCube()
{
    if (!hasAnchor || secondHand == null)
        return;

    Vector3 a = anchorHand.position; // position de la main d'ancrage
    Vector3 b = secondHand.position; // position de la seconde main

    // Taille du cube (valeurs absolues)
    Vector3 size = new Vector3(
        Mathf.Max(minSize, Mathf.Abs(b.x - a.x)),
        Mathf.Max(minSize, Mathf.Abs(b.y - a.y)),
        Mathf.Max(minSize, Mathf.Abs(b.z - a.z))
    );

    // Position du centre du cube = milieu entre les deux mains
    Vector3 center = (a + b) * 0.5f;

    // Appliquer position et taille
    previewObject.transform.position = center;
    previewObject.transform.localScale = size;
}


	void FinalizeCube()
	{
		if (!hasAnchor || secondHand == null)
		{
			return;
		}
		UpdateCube();
		GameObject go = Instantiate(cubePrefab, previewObject.transform.position, previewObject.transform.rotation);
		go.transform.localScale = previewObject.transform.localScale;
		
	}
}