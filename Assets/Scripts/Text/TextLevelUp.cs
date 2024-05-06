using UnityEngine;
using DG.Tweening;

public class TextLevelUp : MonoBehaviour
{
    public void OnEnable()
    {
        transform.DOMove(transform.position + new Vector3(0, 2f), 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}