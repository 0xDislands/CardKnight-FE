using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PopupPoweupUnlocked : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;

    [SerializeField] private Image imgDemo;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtDescription;
   
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowUnlock(PowerupId id)
    {
        if (DataManager.Instance.dicPowerUp.ContainsKey(id) == false)
        {
            Debug.LogError("not found powerup with id = " + id);
            return;
        }
        var data = DataManager.Instance.dicPowerUp[id];

        if (imgDemo) imgDemo.sprite = data.sprite;
        if (txtName) txtName.text = data.name;
        if (txtDescription) txtDescription.text = data.description;

        canvasGroup.DOFade(1f, ANIMATION_TIME);
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);

        var texts = FindObjectsOfType<TextPowerupName>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].UpdateDisplay();
        }
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