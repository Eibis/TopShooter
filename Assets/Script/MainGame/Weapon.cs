using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData WeapData;

    public SpriteRenderer WeaponRenderer;

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
