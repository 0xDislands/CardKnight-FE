using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum GridColor
{
    NoColor, Red, Green, Blue, Orange, Yellow
}

//bao gồm up down left right, nếu là true thì có nghĩa là có ô cùng màu đang tiếp giáp với ô hiện tại
public struct NeightbourData
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public NeightbourData(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }
}

public class GridPosEditor : MonoBehaviour, IPointerDownHandler
{
    public Vector2Int xy;
    public GridPos gridPos;
    public GridColor gridColor = GridColor.NoColor;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gridPos = GetComponent<GridPos>();
    }

    private void Start()
    {
        xy = gridPos.pos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pikachu click me!");
        if (gridColor == GridColor.NoColor)
        {
            gridColor = GridColor.Red;
            spriteRenderer.color = Color.red;
        } else
        {
            gridColor = GridColor.NoColor;
            spriteRenderer.color = Color.white;
        }
    }

    public void Init()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        xy = new Vector2Int(x, y);
        transform.position = new Vector3(x, y);
    }

    //public void UpdateDisplay()
    //{
    //    if (gridColor == GridColor.NoColor) return;

    //    Vector2Int upPos = new Vector2Int(xy.x, xy.y + 1);
    //    Vector2Int downPos = new Vector2Int(xy.x, xy.y - 1);
    //    Vector2Int leftPos = new Vector2Int(xy.x - 1, xy.y);
    //    Vector2Int rightPos = new Vector2Int(xy.x + 1, xy.y);

    //    bool up = GridManager.Instance.dicGridEditors.ContainsKey(upPos) &&
    //        GridManager.Instance.dicGridEditors[upPos].gridColor == gridColor;

    //    bool down = GridManager.Instance.dicGridEditors.ContainsKey(downPos) &&
    //        GridManager.Instance.dicGridEditors[downPos].gridColor == gridColor;

    //    bool left = GridManager.Instance.dicGridEditors.ContainsKey(leftPos) &&
    //        GridManager.Instance.dicGridEditors[leftPos].gridColor == gridColor;

    //    bool right = GridManager.Instance.dicGridEditors.ContainsKey(rightPos) &&
    //        GridManager.Instance.dicGridEditors[rightPos].gridColor == gridColor;

    //    spriteRenderer.sprite = GridManager.Instance.GetSprite(new NeightbourData(up, down, left, right));
    //    spriteRenderer.color = Color.white;
    //}
}