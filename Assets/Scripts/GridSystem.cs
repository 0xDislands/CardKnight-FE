using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private Vector3 offet;
    [ContextMenu("Spawn Grid")]
    public void SpawnGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Instantiate(gridPrefab, new Vector3(i, j),Quaternion.identity,transform);
            }
        }
    }

    [ContextMenu("Fix Postition")]
    public void FixPosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition += offet;
        }
    }
}
