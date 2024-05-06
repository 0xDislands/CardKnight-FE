using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextRunner : MonoBehaviour
{
    public Ease ease = Ease.OutCubic;
    private TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        txt.text = "";
    }

    public void SetValue(string template, int start, int end, float time)
    {
        DOVirtual.Int(start, end, time, (int i) =>
        {
            txt.text = string.Format(template, i);
        }).SetEase(ease);
    }

    public void SetValue(int start, int end, float time)
    {
        SetValue("{0}", start, end, time);
    }
}