﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanoidData", menuName = "ScriptableObjects/HumanoidData", order = 1)]
public class HumanoidData : CharacterData
{
    public Sprite BodyGraphics;
    public Vector2 BodyOffset;
    public int BodyOrder;

    public Sprite HeadGraphics;
    public Vector2 HeadOffset;
    public int HeadOrder;

    public Sprite BackpackGraphics;
    public Vector2 BackpackOffset;
    public int BackpackOrder;

    public Sprite LeftShoulderGraphics;
    public Vector2 LeftShoulderOffset;
    public int LeftShoulderOrder;

    public Sprite RightShoulderGraphics;
    public Vector2 RightShoulderOffset;
    public int RightShoulderOrder;

    public Sprite LeftHandGraphics;
    public Vector2 LeftHandOffset;
    public int LeftHandOrder;

    public Sprite RightHandGraphics;
    public Vector2 RightHandOffset;
    public int RightHandOrder;

    public Vector2 WeaponOffset;
    public int WeaponOrder;

    public float ColliderRadius;
    public Vector2 ColliderOffset;
}