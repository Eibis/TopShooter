using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Weapon ParentWeapon;

    float TTL;
    float Speed;

    float LastFireTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time - LastFireTime > TTL)
            gameObject.SetActive(false);

        if (gameObject.activeSelf)
            transform.position += transform.up * Time.deltaTime * Speed;

        //hit I guess
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("ApplyDamage", 10);
        }
    }
}
