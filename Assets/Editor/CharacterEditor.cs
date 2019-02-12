using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Character character = (Character)target;

        if (GUILayout.Button("Apply Character"))
        {
            character.SetGraphics();
        }

        if (GUILayout.Button("Save Character"))
        {
            character.SaveGraphics();
        }
    }
}