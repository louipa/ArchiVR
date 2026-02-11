using System.Collections;
using UnityEngine;

public class FloorTilesManager : MonoBehaviour
{
    [SerializeField] public GameObject[] blueTiles;
    [SerializeField] public GameObject[] yellowTiles;
    [SerializeField] public GameObject[] redTiles;
    [SerializeField] public GameObject[] greenTiles;

    [SerializeField] public float maxHeight = 5f;
    [SerializeField] public float moveSpeed = 1.5f;

    private bool[] isTriggered = new bool[4];

    public void ActivateBlueTiles()
    {
        ActivateTiles(blueTiles, 0);
    }

    public void ActivateYellowTiles()
    {
        ActivateTiles(yellowTiles, 1);
    }

    public void ActivateRedTiles()
    {
        ActivateTiles(redTiles, 2);
    }

    public void ActivateGreenTiles()
    {
        ActivateTiles(greenTiles, 3);
    }

    private void ActivateTiles(GameObject[] tiles,  int index)
    {
        if (isTriggered[index]) return;
        isTriggered[index] = true;
        
        foreach (GameObject tile in tiles)
        {
            if (tile == null) continue;

            float randomHeight = Random.Range(1f, maxHeight);
            Vector3 targetPosition = tile.transform.position + Vector3.up * randomHeight;

            StartCoroutine(MoveTileUp(tile.transform, targetPosition));
        }
    }

    private IEnumerator MoveTileUp(Transform tile, Vector3 targetPosition)
    {
        Vector3 startPosition = tile.position;
        float elapsedTime = 0f;

        while (Vector3.Distance(tile.position, targetPosition) > 0.01f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            tile.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }

        tile.position = targetPosition;
    }
}