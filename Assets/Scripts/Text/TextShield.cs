using UnityEngine;
using TMPro;

public class TextShield : MonoBehaviour
{
    private TextMeshProUGUI txtHp;

    private void Awake()
    {
        txtHp = GetComponent<TextMeshProUGUI>();
    }
    public void SetShield(HeroData heroData)
    {
        txtHp.text = heroData.shield + "/" + heroData.maxShield;
    }
}