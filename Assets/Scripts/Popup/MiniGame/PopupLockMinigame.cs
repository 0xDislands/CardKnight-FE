using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Dislands;

public class PopupLockMinigame : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;

    [SerializeField] private ParticleSystem appearEffect;
    private List<LockMiniGame_Piece> pieces;
    private LockMiniGame_Ball ball;
    private LockMiniGame_Lock locker;
    private Rotate rotate;
    private CanvasGroup canvasGroup;
    private Card card;
    private CardId idAfterUnlock;
    private void Init()
    {
        pieces = GetComponentsInChildren<LockMiniGame_Piece>().ToList();
        locker = GetComponentInChildren<LockMiniGame_Lock>();
        ball = GetComponentInChildren<LockMiniGame_Ball>();
        rotate = GetComponentInChildren<Rotate>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowMiniGame(Card card, CardId idAfterUnlock)
    {
        this.card = card;
        this.idAfterUnlock = idAfterUnlock;

        Init();
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].Lock();
        }
        gameObject.SetActive(true);
        rotate.enabled = true;
        canvasGroup.DOFade(1f, ANIMATION_TIME);
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);

        DOVirtual.Float(200, 300, 0.5f, (float f) =>
        {
            rotate.spinSpeed = f;
        });
    }

    public void OnClick()
    {
        locker.Bounce();
        if (ball.currentPiece == null)
        {
            Close();
            CardManager.Instance.hero.TakeDamage(new DamageData(1), out bool dead);
        } else
        {
            ball.currentPiece.Unlock();
            if (IsAllUnlocked())
            {
                CardManager.Instance.SpawnCard(card.Pos, idAfterUnlock);
                card.Disappear();
                var effect = SimpleObjectPool.Instance.GetObjectFromPool(appearEffect, card.transform.position);
                Close();
            }
        }
    }

    public bool IsAllUnlocked()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].unlocked == false) return false;
        }
        return true;
    }

    public void Close()
    {
        canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}