using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPos : MonoBehaviour
{
    public const bool TEST_MODE = true;

    public Vector2Int pos;
    public TextMeshProUGUI txtDebug;
    public Card card;

    private void Awake()
    {
        txtDebug = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        txtDebug.text = pos.ToString();
        if (card == null) return;
        if (card.Pos != pos)
        {
            Debug.LogError($"wrong position at {card}, pos = {pos}");
        }
    }
}