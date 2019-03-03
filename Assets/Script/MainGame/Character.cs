using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData CharData;

    public Weapon Weapon;

    /**/

    public float Damage = 0.0f;

    /**/

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

    public float Health
    {
        get
        {
            return CharData.Health;
        }
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
