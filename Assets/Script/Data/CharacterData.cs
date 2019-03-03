using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public float BaseSpeed = 5.0f;
    public float RotationSpeed = 10.0f;
    public float MaxSpeed = 10.0f;
    public float Health = 100.0f;
}