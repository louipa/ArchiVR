using System.Collections;
using UnityEngine;

public class FloorTilesManager : MonoBehaviour
{
    [SerializeField] public GameObject[] blueTiles;
    [SerializeField] public GameObject[] yellowTiles;
    [SerializeField] public GameObject[] redTiles;
    [SerializeField] public GameObject[] greenTiles;
    [SerializeField] public Material blueMaterial;
    [SerializeField] public Material yellowMaterial;
    [SerializeField] public Material redMaterial;
    [SerializeField] public Material greenMaterial;

    [SerializeField] public float maxHeight = 5f;
    [SerializeField] public float moveSpeed = 1.5f;

    private bool[] isTriggered = new bool[4];

    private void ChangeTilesMaterial(GameObject[] tiles, Material  material)
    {
        foreach (GameObject tile in tiles)
            tile.GetComponent<Renderer>().material = material;
    }

    public void ActivateBlueTiles()
    {
        ActivateTiles(blueTiles, 0);
        ChangeTilesMaterial(blueTiles, blueMaterial);
    }

    public void ActivateYellowTiles()
    {
        ActivateTiles(yellowTiles, 1);
        ChangeTilesMaterial(yellowTiles, yellowMaterial);
    }

    public void ActivateRedTiles()
    {
        ActivateTiles(redTiles, 2);
        ChangeTilesMaterial(redTiles, redMaterial);
    }

    public void ActivateGreenTiles()
    {
        ActivateTiles(greenTiles, 3);
        ChangeTilesMaterial(greenTiles, greenMaterial);
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