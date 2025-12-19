using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabEvent : MonoBehaviour
{
    XRGrabInteractable _grab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _grab.selectEntered.AddListener(OnGrab);
        Debug.Log("GrabEvent Awake");
    }
   
    
    void OnGrab(SelectEnterEventArgs args)
    {
        StartCoroutine(PlotDashboard());
    }
    
    IEnumerator PlotDashboard()
    {
        yield return new WaitForSeconds(5f);
        Position_Tracker.PrintLastPositions();
    }
}
