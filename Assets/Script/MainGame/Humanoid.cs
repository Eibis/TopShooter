using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Humanoid : Character
{
    public SpriteRenderer BodyRenderer;
    public SpriteRenderer HeadRenderer;
    public SpriteRenderer BackpackRenderer;
    public SpriteRenderer LeftShoulderRenderer;
    public SpriteRenderer RightShoulderRenderer;
    public SpriteRenderer LeftHandRenderer;
    public SpriteRenderer RightHandRenderer;

    public SpriteRenderer WeaponRenderer;

    public CircleCollider2D Collider;

    public Animator Anim;

    public int CollisionLayer;

    RaycastHit2D[] HitsBuffer = new RaycastHit2D[10];

    Vector2 MovementVec;

    public void Awake()
    {
        CollisionLayer = 1 << LayerMask.NameToLayer("Character");

        SetGraphics();
    }

    public void SetGraphics()
    {
        HumanoidData data = CharData as HumanoidData;
        BodyRenderer.sprite = data.BodyGraphics;
        BodyRenderer.transform.localPosition = data.BodyOffset;
        BodyRenderer.sortingOrder = data.BodyOrder;

        HeadRenderer.sprite = data.HeadGraphics;
        HeadRenderer.transform.localPosition = data.HeadOffset;
        HeadRenderer.sortingOrder = data.HeadOrder;

        BackpackRenderer.sprite = data.BackpackGraphics;
        BackpackRenderer.transform.localPosition = data.BackpackOffset;
        BackpackRenderer.sortingOrder = data.BackpackOrder;

        LeftShoulderRenderer.sprite = data.LeftShoulderGraphics;
        LeftShoulderRenderer.transform.localPosition = data.LeftShoulderOffset;
        LeftShoulderRenderer.sortingOrder = data.LeftShoulderOrder;

        RightShoulderRenderer.sprite = data.RightShoulderGraphics;
        RightShoulderRenderer.transform.localPosition = data.RightShoulderOffset;
        RightShoulderRenderer.sortingOrder = data.RightShoulderOrder;

        LeftHandRenderer.sprite = data.LeftHandGraphics;
        LeftHandRenderer.transform.localPosition = data.LeftHandOffset;
        LeftHandRenderer.sortingOrder = data.LeftHandOrder;

        RightHandRenderer.sprite = data.RightHandGraphics;
        RightHandRenderer.transform.localPosition = data.RightHandOffset;
        RightHandRenderer.sortingOrder = data.RightHandOrder;

        WeaponRenderer.transform.localPosition = data.WeaponOffset;
        WeaponRenderer.sortingOrder = data.WeaponOrder;

        Collider.offset = data.ColliderOffset;
        Collider.radius = data.ColliderRadius;
    }

#if UNITY_EDITOR
    public void SaveGraphics()
    {
        HumanoidData data = CharData as HumanoidData;
        data.BodyGraphics = BodyRenderer.sprite;
        data.BodyOffset = BodyRenderer.transform.localPosition;
        data.BodyOrder = BodyRenderer.sortingOrder;

        data.HeadGraphics = HeadRenderer.sprite;
        data.HeadOffset = HeadRenderer.transform.localPosition;
        data.HeadOrder = HeadRenderer.sortingOrder;

        data.BackpackGraphics = BackpackRenderer.sprite;
        data.BackpackOffset = BackpackRenderer.transform.localPosition;
        data.BackpackOrder = BackpackRenderer.sortingOrder;

        data.LeftShoulderGraphics = LeftShoulderRenderer.sprite;
        data.LeftShoulderOffset = LeftShoulderRenderer.transform.localPosition;
        data.LeftShoulderOrder = LeftShoulderRenderer.sortingOrder;

        data.RightShoulderGraphics = RightShoulderRenderer.sprite;
        data.RightShoulderOffset = RightShoulderRenderer.transform.localPosition;
        data.RightShoulderOrder = RightShoulderRenderer.sortingOrder;

        data.LeftHandGraphics = LeftHandRenderer.sprite;
        data.LeftHandOffset = LeftHandRenderer.transform.localPosition;
        data.LeftHandOrder = LeftHandRenderer.sortingOrder;

        data.RightHandGraphics = RightHandRenderer.sprite;
        data.RightHandOffset = RightHandRenderer.transform.localPosition;
        data.RightHandOrder = RightHandRenderer.sortingOrder;

        data.WeaponOffset = WeaponRenderer.transform.localPosition;
        data.WeaponOrder = WeaponRenderer.sortingOrder;

        data.ColliderOffset = Collider.offset;
        data.ColliderRadius = Collider.radius;

        EditorUtility.SetDirty(CharData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

    protected void LateUpdate()
    {
        CheckCollision();
    }

    public void Move(float hor_inp, float ver_inp)
    {
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

        transform.Translate((Vector3)MovementVec, Space.World);

        CheckCollision();

        Anim.SetFloat("Speed", MovementVec.magnitude);

        MovementVec = Vector2.zero;
    }

    public void CheckCollision()
    {
        int n_hit = Physics2D.CircleCastNonAlloc((Vector2)(transform.position + transform.TransformDirection(Collider.offset)), Collider.radius, transform.forward, HitsBuffer, Mathf.Infinity, CollisionLayer);

        if (n_hit > 1)
        {
            Vector2 adjustment = Vector2.zero;

            float adj_amount = MovementVec.magnitude > 0 ? MovementVec.magnitude : (0.5f * Speed * Time.deltaTime);

            for (int i = 0; i < n_hit; ++i)
            {
                if (HitsBuffer[i].collider == Collider)
                    continue;

                adjustment += HitsBuffer[i].normal * adj_amount;
            }

            transform.Translate((Vector3)adjustment, Space.World);
        }
    }
}
