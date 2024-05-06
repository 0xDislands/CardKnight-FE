using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PopupGameOver : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;
    public const float RUN_TEXT_TIME = 2f;

    [SerializeField] private TextRunner txtHighScore;
    [SerializeField] private TextRunner txtYourRank;
    [SerializeField] private TextRunner txtReward;
    [SerializeField] private Button btnRetry;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StartCoroutine(IEDoEffect());
    }

    public IEnumerator IEDoEffect()
    {
        canvasGroup.DOFade(1f, ANIMATION_TIME);
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);
        btnRetry.gameObject.SetActive(false);

        var exp = CardManager.Instance.hero.heroData.totalExp;
        txtHighScore.SetValue(0, (int)exp, RUN_TEXT_TIME);
        yield return new WaitForSeconds(1f);

        int rank = 2;
        txtYourRank.SetValue("{0}/9", 9, rank, RUN_TEXT_TIME);
        yield return new WaitForSeconds(1f);

        int reward = Random.Range(150, 300);
        txtReward.SetValue(0, reward, RUN_TEXT_TIME);
        yield return new WaitForSeconds(3f);

        btnRetry.gameObject.SetActive(true);
        btnRetry.transform.localScale = Vector2.zero;
        btnRetry.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

    public void Close()
    {
        canvasGroup.DOFade(0f, ANIMATION_TIME);
        transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(() => {
            gameObject.SetActive(false);
            PopupManager.Instance.DoNextAction();
        });
    }
}