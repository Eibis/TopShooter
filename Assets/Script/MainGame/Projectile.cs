using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public CircleCollider2D Collider;

    Weapon ParentWeapon;

    float TTL;
    float Speed;

    float LastFireTime = 0;

    RaycastHit2D[] HitsBuffer = new RaycastHit2D[5];

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time - LastFireTime > TTL)
            Expire();

        if (gameObject.activeSelf)
            transform.position += transform.up * Time.deltaTime * Speed;

        int n_hit = Physics2D.CircleCastNonAlloc(transform.position, Collider.radius, transform.forward, HitsBuffer);

        if (n_hit > 1)
        {
            for(int i = 0; i < n_hit; ++i)
            {
                if (HitsBuffer[i].collider == Collider)
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
