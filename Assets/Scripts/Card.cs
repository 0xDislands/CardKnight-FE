using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Dislands;

public class Card : MonoBehaviour, IPointerDownHandler
{
    public const bool DEBUG_POSITION = false;
    public const float SPAWN_SPAW_X = -4f;
    public const float SPAWN_SPAW_Y = 8f;
    public const float FLIP_ANIMATION_TIME = 0.3f;
    public const float CARD_MOVE_SPEED = 0.6f;
    public const float CARD_FADE_SPEED = 0.4f;
    public const float SPAWN_DELAY_FLIP = 0.2f;

    private Vector2Int pos;
    [SerializeField] Image cardBack;
    [SerializeField] Transform cardParent;

    public CardData data { get; private set; }
    public CardEffect cardEffect { get; private set; }
    private CanvasGroup canvasGroup;
    private List<CardEffect> effects = new List<CardEffect>();

    public Vector2Int Pos
    {
        get { return pos; }
        set
        {
            pos = value;
            GridManager.Instance.dicGrids[pos].card = this;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardEffect = GetComponent<CardEffect>();
    }

    public void SetData(CardData cardData)
    {
        this.data = cardData;
    }
    public void ShowSpawnAnimation(float delayFlip = SPAWN_DELAY_FLIP)
    {
        cardBack.gameObject.SetActive(true);
        Vector2 startPos = transform.position;
        transform.position = transform.position + new Vector3(SPAWN_SPAW_X, SPAWN_SPAW_Y);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(startPos, 0.8f));
        sequence.AppendInterval(delayFlip);
        sequence.AppendCallback(() =>
        {
            FlipToFront();
        });
    }

    public void FlipToFront()
    {
        cardBack.gameObject.SetActive(true);
        transform.DOScaleX(0f, FLIP_ANIMATION_TIME).OnComplete(() =>
        {
            transform.DOScaleX(1f, FLIP_ANIMATION_TIME);
            cardBack.gameObject.SetActive(false);
        });
    }

    public void FlipToBack()
    {
        cardBack.gameObject.SetActive(false);
        transform.DOScaleX(0f, FLIP_ANIMATION_TIME).OnComplete(() =>
        {
            transform.DOScaleX(1f, FLIP_ANIMATION_TIME);
            cardBack.gameObject.SetActive(true);
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardManager.Instance.IsNextToHeroCard(this))
        {
            CardManager.Instance.UseCard(this);
        }
        var hero = GetComponent<Hero>();
        if (hero != null)
        {
            Debug.Log("You click hero card");
        }
    }

    public void Disappear()
    {
        Bounce();
        canvasGroup.DOFade(0f, CARD_FADE_SPEED).OnComplete(() =>
        {
            gameObject.SetActive(false);
            if (GridManager.Instance.dicGrids[pos].card == this)
            {
                GridManager.Instance.dicGrids[pos].card = null;
            }
        });
    }
    private void Bounce()
    {
        transform.localScale = Vector3.one;
        EasyEffect.Bounce(gameObject, 0.1f, strength: 0.1f);
    }
    public void MoveToPos(Vector2Int pos)
    {
        this.pos = pos;
        GridManager.Instance.dicGrids[pos].card = this;
        LeanTween.move(transform.gameObject, GridManager.Instance.dicGrids[pos].transform.position, CARD_MOVE_SPEED);
    }
}
