using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PopupLevelUp : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;

    public Transform levelUpDataParent;
    public List<LevelUpOption> options;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowLevelUp(int index)
    {
        canvasGroup.DOFade(1f, ANIMATION_TIME);
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);
        if (index >= levelUpDataParent.transform.childCount)
        {
            Debug.LogError("not enough data to show: " + index);
            index = levelUpDataParent.transform.childCount - 1;
        }
        var step = levelUpDataParent.GetChild(index);
        for (int i = 0; i < options.Count; i++)
        {
            if (i < step.childCount)
            {
                var levelUpData = step.GetChild(i).GetComponent<LevelUpData>();
                options[i].Show(levelUpData);
            } else
            {
                options[i].gameObject.SetActive(false);
            }
        }
    }

    public void Close()
    {
        canvasGroup.DOFade(0f, ANIMATION_TIME);
        transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(()=> {
            gameObject.SetActive(false);
            PopupManager.Instance.DoNextAction();
        });
    }
}