using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpOption : MonoBehaviour
{
    public const int FULL_LIMIT = 9999;
    public Image imgDemo;
    public TextMeshProUGUI title;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI description;
    private LevelUpData data;
    [SerializeField] private PopupLevelUp popupLevelUp;

    private void Awake()
    {
        popupLevelUp = GetComponentInParent<PopupLevelUp>();
    }

    public void Show(LevelUpData data)
    {
        this.data = data;
        imgDemo.sprite = data.sprite;
        title.text = data.title;
        description.text = data.description;
        if (data.amount >= FULL_LIMIT)
        {
            amount.text = "FULL";
        } else
        {
            amount.text = data.amount.ToString();
        }
    }

    public void OnClick()
    {
        switch (data.id)
        {
            case LevelUpId.ADD_HP:
                var damage = new DamageData();
                damage.damage = data.amount;
                CardManager.Instance.hero.AddHP(damage);
                break;
            case LevelUpId.ADD_AMOUR:
                CardManager.Instance.hero.AddShield(data.amount);
                break;
            case LevelUpId.INCREASE_MAX_HP:
                CardManager.Instance.hero.heroData.maxHp += data.amount;
                CardManager.Instance.hero.UpdateDisplay ();
                break;
            case LevelUpId.INCREASE_MAX_AMOUR:
                CardManager.Instance.hero.heroData.maxShield += data.amount;
                CardManager.Instance.hero.UpdateDisplay ();
                break;
        }
        popupLevelUp.Close();
    }
}