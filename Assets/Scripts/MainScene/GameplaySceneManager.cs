using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    MainScene, Gameplay
}

public class GameplaySceneManager : MonoBehaviour
{
    public const string SCENE_GAMEPLAY = "Gameplay";
    public Canvas canvasStart;

    private void Start()
    {
        ShowMainScene();
    }

    public void Play()
    {
        var canvasGroup = canvasStart.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, 1f).OnComplete(() =>
        {
            canvasStart.gameObject.SetActive(false);
        });
        Gameplay.Instance.StartGame();
    }

    public void ShowMainScene()
    {
        canvasStart.gameObject.SetActive(true);
        canvasStart.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }
}