using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    public List<System.Action> actions = new List<System.Action>();
    private int index = 0;

    private void Awake()
    {
        Instance = this;
    }
    public void ShowInQueue(System.Action action)
    {
        actions.Add(action);
        if (index == 0)
        {
            actions[index]?.Invoke();
        }
    }

    public void DoNextAction()
    {
        if (actions.Count == 0) return;
        actions.RemoveAt(0);
        if (actions.Count > 0)
        {
            actions[0].Invoke();
        }
    }
}