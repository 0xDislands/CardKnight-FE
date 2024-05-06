using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EffectSlash : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void DoEffect()
    {
        transform.position += new Vector3(0.5f, 0.5f);
        spriteRenderer.color = Color.clear;
        spriteRenderer.DOColor(Color.white, 0.2f).OnComplete(() =>
        {
            spriteRenderer.DOFade(0f, 1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        });
        transform.DOMove(transform.position + new Vector3(-1f, -1f), 1f).SetEase(Ease.OutSine);
    }
}