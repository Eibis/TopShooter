using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData WeapData;

    public SpriteRenderer WeaponRenderer;

    public GameObject ProjectilePrefab;

    /*TODO projectile pool and setting infos like damage*/

    public void SetGraphics()
    {
        WeaponRenderer.sprite = WeapData.Graphics;
    }

#if UNITY_EDITOR

    public void SaveGraphics()
    {
        WeapData.Graphics = WeaponRenderer.sprite;
    }

#endif

}
