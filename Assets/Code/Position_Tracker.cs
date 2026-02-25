using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.UI;

public class Position_Tracker : MonoBehaviour
{
    [SerializeField] private float recordInterval = 1f; // seconds
    
    private static readonly List<Vector3> LastPositions = new(10000);
    private float _timer;
    
    
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < recordInterval) return;
        
        _timer = 0f;
        LastPositions.Add(transform.position);
    }


    public static List<float> GetLastSpeeds()
    {
        var speeds = new List<float>();

        if (LastPositions.Count < 2)
            return speeds;

        
        for (int i = 1; i < LastPositions.Count; i++)
        {
            float speed = Vector3.Distance(LastPositions[i - 1], LastPositions[i]); // recordInterval = 1
            speeds.Add(speed);
        }

        return speeds;
    }
}