using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public CircleCollider2D Collider;

    [HideInInspector]
    public int CollisionLayer;

    Weapon ParentWeapon;

    float TTL;
    float Speed;

    float LastFireTime = 0;

    RaycastHit2D[] HitsBuffer = new RaycastHit2D[5];

    Material ScrollableMaterial;
    float CurrentScroll = 0.0f;
    float ScrollSpeed = 5.0f;

    void Start()
    {
        CollisionLayer = 1 << LayerMask.NameToLayer("Character");
        ScrollableMaterial = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        CurrentScroll += ScrollSpeed * Time.deltaTime;
        foreach (var texture_name in ScrollableMaterial.GetTexturePropertyNames())
        {
            ScrollableMaterial.SetTextureOffset(texture_name, new Vector2(0, -CurrentScroll));
        }
        //https://answers.unity.com/questions/605905/dynamic-offset-tiling-for-spriterenderer.html
        //https://forum.unity.com/threads/is-it-still-possible-to-scroll-sprite-textures-with-material-maintextureoffset.451947/
        if (Time.time - LastFireTime > TTL)
            Expire();

        if (gameObject.activeSelf)
            transform.Translate(transform.up * Time.deltaTime * Speed, Space.World);

        int n_hit = Physics2D.CircleCastNonAlloc(transform.position + transform.TransformDirection(Collider.offset), Collider.radius, transform.forward, HitsBuffer, Mathf.Infinity, CollisionLayer);

        if (n_hit > 0)
        {
            for(int i = 0; i < n_hit; ++i)
            {
                if (HitsBuffer[i].collider == Collider)
                    continue;

                if (ParentWeapon.CollidersToIgnore.Contains(HitsBuffer[i].collider))
                    continue;

                Hit(HitsBuffer[i].collider);
            }
        }
    }

    internal void Init(Weapon weapon)
    {
        ParentWeapon = weapon;

        TTL = weapon.ProjectileTTL;
        Speed = weapon.ProjectileSpeed;

        gameObject.SetActive(false);
    }

    internal void Fire()
    {
        transform.position = (Vector2)(ParentWeapon.transform.position + ParentWeapon.transform.TransformDirection(ParentWeapon.WeapData.FirePositionOffset));
        transform.rotation = ParentWeapon.transform.rotation;

        gameObject.SetActive(true);

        LastFireTime = Time.time;
    }

    private void Hit(Collider2D collider)
    {
        Debug.Log(collider.name);

        Expire();
    }

    private void Expire()
    {
        gameObject.SetActive(false);
    }

}
