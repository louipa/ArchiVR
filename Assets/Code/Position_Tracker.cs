using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.UI;

public class Position_Tracker : MonoBehaviour
{
    [SerializeField] private float recordInterval = 1f; // seconds
    
    private static readonly Queue<Vector3> LastPositions = new(10000);
    private float _timer;
    
    
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < recordInterval) return;
        
        _timer = 0f;
        LastPositions.Enqueue(transform.position);
    }

    public static void PrintLastPositions()
    {
        //TODO pretty print of graph + interface
        Debug.Log("Last recorded player positions:");
        var i = 1;
        foreach (var pos in LastPositions)
        {
            Debug.Log($"{i}: {pos}");
            i++;
        }
    }
    public static List<float> GetLastSpeeds()
    {
        var speeds = new List<float>();

        if (LastPositions.Count < 2)
            return speeds;

        Vector3 previous = Vector3.zero;
        bool first = true;

        foreach (var current in LastPositions)
        {
            if (first)
            {
                previous = current;
                first = false;
                continue;
            }

            float distance = Vector3.Distance(previous, current);
            float speed = distance / 1; // recordInterval = 1

            speeds.Add(speed);
            previous = current;
        }

        return speeds;
    }
}