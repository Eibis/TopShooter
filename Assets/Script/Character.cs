using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData CharData;

    public SpriteRenderer BodyRenderer;
    public SpriteRenderer HeadRenderer;
    public SpriteRenderer BackpackRenderer;
    public SpriteRenderer LeftShoulderRenderer;
    public SpriteRenderer RightShoulderRenderer;
    public SpriteRenderer LeftHandRenderer;
    public SpriteRenderer RightHandRenderer;

    public CharacterInput Input;

    public WeaponData WeapData;

    public CircleCollider2D Collider;

    public float Speed
    {
        get
        {
            return CharData.BaseSpeed;
        }
    }

    void Awake()
    {
        Input.CharacterRef = this;

        SetGraphics();
    }

    public void SetGraphics()
    {
        BodyRenderer.sprite = CharData.BodyGraphics;
        BodyRenderer.transform.localPosition = CharData.BodyOffset;
        BodyRenderer.sortingOrder = CharData.BodyOrder;

        HeadRenderer.sprite = CharData.HeadGraphics;
        HeadRenderer.transform.localPosition = CharData.HeadOffset;
        HeadRenderer.sortingOrder = CharData.HeadOrder;

        BackpackRenderer.sprite = CharData.BackpackGraphics;
        BackpackRenderer.transform.localPosition = CharData.BackpackOffset;
        BackpackRenderer.sortingOrder = CharData.BackpackOrder;

        LeftShoulderRenderer.sprite = CharData.LeftShoulderGraphics;
        LeftShoulderRenderer.transform.localPosition = CharData.LeftShoulderOffset;
        LeftShoulderRenderer.sortingOrder = CharData.LeftShoulderOrder;

        RightShoulderRenderer.sprite = CharData.RightShoulderGraphics;
        RightShoulderRenderer.transform.localPosition = CharData.RightShoulderOffset;
        RightShoulderRenderer.sortingOrder = CharData.RightShoulderOrder;

        LeftHandRenderer.sprite = CharData.LeftHandGraphics;
        LeftHandRenderer.transform.localPosition = CharData.LeftHandOffset;
        LeftHandRenderer.sortingOrder = CharData.LeftHandOrder;

        RightHandRenderer.sprite = CharData.RightHandGraphics;
        RightHandRenderer.transform.localPosition = CharData.RightHandOffset;
        RightHandRenderer.sortingOrder = CharData.RightHandOrder;

        Collider.offset = CharData.ColliderOffset;
        Collider.radius = CharData.ColliderRadius;
    }

#if UNITY_EDITOR
    public void SaveGraphics()
    {
        CharData.BodyGraphics = BodyRenderer.sprite;
        CharData.BodyOffset = BodyRenderer.transform.localPosition;
        CharData.BodyOrder = BodyRenderer.sortingOrder;

        CharData.HeadGraphics = HeadRenderer.sprite;
        CharData.HeadOffset= HeadRenderer.transform.localPosition;
        CharData.HeadOrder = HeadRenderer.sortingOrder;

        CharData.BackpackGraphics = BackpackRenderer.sprite;
        CharData.BackpackOffset = BackpackRenderer.transform.localPosition;
        CharData.BackpackOrder = BackpackRenderer.sortingOrder;

        CharData.LeftShoulderGraphics = LeftShoulderRenderer.sprite;
        CharData.LeftShoulderOffset = LeftShoulderRenderer.transform.localPosition;
        CharData.LeftShoulderOrder = LeftShoulderRenderer.sortingOrder;

        CharData.RightShoulderGraphics = RightShoulderRenderer.sprite;
        CharData.RightShoulderOffset = RightShoulderRenderer.transform.localPosition;
        CharData.RightShoulderOrder = RightShoulderRenderer.sortingOrder;

        CharData.LeftHandGraphics = LeftHandRenderer.sprite;
        CharData.LeftHandOffset = LeftHandRenderer.transform.localPosition;
        CharData.LeftHandOrder = LeftHandRenderer.sortingOrder;

        CharData.RightHandGraphics = RightHandRenderer.sprite;
        CharData.RightHandOffset = RightHandRenderer.transform.localPosition;
        CharData.RightHandOrder = RightHandRenderer.sortingOrder;

        CharData.ColliderOffset = Collider.offset;
        CharData.ColliderRadius = Collider.radius;
    }
#endif

    void Update()
    {
        
    }
}
