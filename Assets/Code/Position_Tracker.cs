using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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

    private void OnApplicationQuit()
    {
        PrintLastPositions();
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


}