using UnityEngine;
using UnityEngine.UI;

public class LockMiniGame_Piece : MonoBehaviour
{
    public Color colorLock;
    public Color colorUnlock;
    private Image img;
    public bool unlocked { get; private set; } = false;

    private void Awake()
    {
        if (img == null) img = GetComponent<Image>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var ball = collision.GetComponent<LockMiniGame_Ball>();
        if (ball != null)
        {
            ball.currentPiece = this;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var ball = collision.GetComponent<LockMiniGame_Ball>();
        if (ball != null)
        {
            if (ball.currentPiece == this) ball.currentPiece = null;
        }
    }

    public void Unlock()
    {
        unlocked = true;
        if (img == null) img = GetComponent<Image>();
        img.color = colorUnlock;
    }

    public void Lock()
    {
        unlocked = false;
        if (img == null) img = GetComponent<Image>();
        img.color = colorLock;
    }
}