using DG.Tweening;
using UnityEngine;

public class LockMiniGame_Lock : MonoBehaviour
{
    Vector2 originalScale;
    private void Awake()
    {
        originalScale = transform.localScale;    
    }
    public void Bounce()
    {
        transform.DOScale(originalScale * 1.05f, 0.2f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutCubic);
        });
    }
}