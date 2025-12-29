using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using XCharts.Runtime;


public class GrabEvent : MonoBehaviour
{
    [SerializeField] private Canvas _uiCanvas;
    [SerializeField] private LineChart _lineChart;
    
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

        List<float> speeds = Position_Tracker.GetLastSpeeds();
        
        _lineChart.GetChartComponent<YAxis>().axisLabel.formatter = "{value:F2}";
        _lineChart.RemoveData();
        if (_lineChart.series.Count == 0)
        {
            _lineChart.AddSerie<Line>("Speed");
        }

        for (int i = 0; i < speeds.Count; i++)
        {
            _lineChart.AddData(0, i, speeds[i]);
        }

        _lineChart.RefreshChart();
        
        Debug.Log(_uiCanvas.gameObject.activeSelf);
        _uiCanvas.gameObject.SetActive(true);
        Debug.Log(_uiCanvas.gameObject.activeSelf);
        
    }
}
