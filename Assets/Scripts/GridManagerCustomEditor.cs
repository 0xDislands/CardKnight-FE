#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GridManager grid = (GridManager)target;
        if (GUILayout.Button("SET UP GRID", GUILayout.Width(150), GUILayout.Height(50))) 
        {
            grid.Init();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}

#endif