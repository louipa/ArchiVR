using System.Collections;
using System.Collections.Generic;
using Tengio;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using XCharts.Runtime;


public class GrabEvent : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private LineChart lineChart;
    [SerializeField] private TeleportManager teleportManager;
    
    XRGrabInteractable _grab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _grab.selectEntered.AddListener(OnGrab);
    }
   
    
    void OnGrab(SelectEnterEventArgs args)
    {
        // StartCoroutine(PlotDashboard());
    }
    
    IEnumerator PlotDashboard()
    {
        yield return new WaitForSeconds(2f);
        List<float> speeds = Position_Tracker.GetLastSpeeds();
        
        lineChart.GetChartComponent<YAxis>().axisLabel.formatter = "{value:F2}";
        lineChart.RemoveData();
        if (lineChart.series.Count == 0)
        {
            lineChart.AddSerie<Line>("Speed");
        }

        for (int i = 0; i < speeds.Count; i++)
        {
            lineChart.AddData(0, i, speeds[i]);
        }
        lineChart.RefreshChart();
        uiCanvas.gameObject.SetActive(true);
    }
}
