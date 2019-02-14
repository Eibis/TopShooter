using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public Sprite Graphics;

    public Vector2 FirePositionOffset;

    public GameObject ProjectilePrefab;

    public float FireCooldown;

    public float ProjectileTTL;
    public float ProjectileSpeed;
}
