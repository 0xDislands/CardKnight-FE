using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dislands;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public const int WIDTH = 3;

    public GridPos[] grids { get; private set; }
    public Dictionary<Vector2Int, GridPos> dicGrids { get; private set; } = new Dictionary<Vector2Int, GridPos>();
    public List<Vector2Int> cornerPositions = new List<Vector2Int>();

    private void Awake()
    {
        Instance = this;
        Init();
    }
    public void Init()
    {
        cornerPositions = new List<Vector2Int>();
        cornerPositions.Add(new Vector2Int(0, 0));
        cornerPositions.Add(new Vector2Int(WIDTH - 1, 0));
        cornerPositions.Add(new Vector2Int(0, WIDTH - 1));
        cornerPositions.Add(new Vector2Int(WIDTH - 1, WIDTH - 1));

        grids = GetComponentsInChildren<GridPos>();
        dicGrids = new Dictionary<Vector2Int, GridPos>();
        for (int i = 0; i < grids.Length; i++)
        {
            int x = i % WIDTH;
            int y = i / WIDTH;
            grids[i].pos = new Vector2Int(x, y);
            dicGrids.Add(grids[i].pos, grids[i]);
        }
    }

    public bool IsInsideGrid(Vector2Int pos)
    {
        return dicGrids.ContainsKey(pos);
    }

    public bool IsCornerCard(Vector2Int pos)
    {
        return cornerPositions.Contains(pos);
    }
}