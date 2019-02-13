using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Weapon weapon = (Weapon)target;

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Apply Weapon"))
        {
            weapon.SetGraphics();
        }

        if (GUILayout.Button("Save Weapon"))
        {
            weapon.SaveGraphics();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }
}