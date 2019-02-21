using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    public SpriteRenderer WeaponRenderer;

    public CharacterInput Input;

    public Weapon Weapon;

    public CircleCollider2D Collider;

    public Animator Anim;

    public int CollisionLayer;

    RaycastHit2D[] HitsBuffer = new RaycastHit2D[10];

    Vector2 MovementVec;
    
    public float RotationSpeed
    {
        get
        {
            return CharData.RotationSpeed;
        }
    }

    public float Speed
    {
        get
        {
            return CharData.BaseSpeed;
        }
    }

    public float MaxSpeed
    {
        get
        {
            return CharData.MaxSpeed;
        }
    }

    void Awake()
    {
        Input.CharacterRef = this;
        CollisionLayer = 1 << LayerMask.NameToLayer("Character");

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

        WeaponRenderer.transform.localPosition = CharData.WeaponOffset;
        WeaponRenderer.sortingOrder = CharData.WeaponOrder;
        
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

        CharData.WeaponOffset = WeaponRenderer.transform.localPosition;
        CharData.WeaponOrder = WeaponRenderer.sortingOrder;

        CharData.ColliderOffset = Collider.offset;
        CharData.ColliderRadius = Collider.radius;

        EditorUtility.SetDirty(CharData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

    public void Move(float hor_inp, float ver_inp)
    {
        MovementVec = new Vector2(0.0f, 0.0f);

        float scaled_speed = Time.deltaTime * Speed;

        if (hor_inp != 0.0f)
        {
            MovementVec += new Vector2(scaled_speed * hor_inp, 0.0f);
        }

        if (ver_inp != 0.0f)
        {
            MovementVec += new Vector2(0.0f, scaled_speed * ver_inp);
        }

        if (MovementVec.magnitude > MaxSpeed)
            MovementVec = MovementVec.normalized * MaxSpeed;

        int n_hit = Physics2D.CircleCastNonAlloc((Vector2)(transform.position + transform.TransformDirection(Collider.offset)) + MovementVec, Collider.radius, transform.forward, HitsBuffer, Mathf.Infinity, CollisionLayer);

        if (n_hit > 1)
        {
            for (int i = 0; i < n_hit; ++i)
            {
                if (HitsBuffer[i].collider == Collider)
                    continue;

                MovementVec += HitsBuffer[i].normal * MovementVec.magnitude;
            }
        }

        transform.position += (Vector3)MovementVec;

        Anim.SetFloat("Speed", MovementVec.magnitude);
    }

    internal void Rotate(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    public void Fire()
    {
        Weapon.Fire();
    }
}
