using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Humanoid))]
public class CharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Humanoid character = (Humanoid)target;

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Apply Character"))
        {
            character.SetGraphics();
        }

        if (GUILayout.Button("Save Character"))
        {
            character.SaveGraphics();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }
}