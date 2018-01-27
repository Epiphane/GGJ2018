using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FuzzyRounder))]
public class FuzzyRounderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        FuzzyRounder round_boi = (FuzzyRounder)target;
        if(GUILayout.Button("ROUND IT OFF"))
        {
            round_boi.DoIt();
        }
    }
}