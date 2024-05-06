using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMove : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Transform moveObj;
    public Ease ease;
    public float time;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        DoEffect();
    }

    public void DoEffect()
    {
        moveObj.transform.position = startPos.transform.position;
        moveObj.DOMove(endPos.transform.position, time).SetEase(ease);
    }
}