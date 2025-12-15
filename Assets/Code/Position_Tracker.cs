using UnityEngine;
using System.Collections.Generic;

public class Position_Tracker : MonoBehaviour
{
    [SerializeField] private float recordInterval = 1f; // seconds

    private readonly Queue<Vector3> _lastPositions = new(10000);
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < recordInterval) return;
        
        _timer = 0f;
        _lastPositions.Enqueue(transform.position);
    }

    private void OnApplicationQuit()
    {
        PrintLastPositions();
    }

    private void PrintLastPositions()
    {
        //TODO we will have to store it somewhere at some point
        Debug.Log("Last recorded player positions:");

        var i = 1;
        foreach (var pos in _lastPositions)
        {
            Debug.Log($"{i}: {pos}");
            i++;
        }
    }
}