using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Weapon : MonoBehaviour
{
    public WeaponData WeapData;

    public SpriteRenderer WeaponRenderer;

    public int ProjectilePoolSize = 20;
    List<Projectile> Projectiles;

    public float LastShootTime = 0;

    public float FireCooldown;

    public float ProjectileTTL;
    public float ProjectileSpeed;

    public List<Collider2D> CollidersToIgnore;

    private void Start()
    {
        SetGraphics();

        Projectiles = new List<Projectile>();

        for (int i = 0; i < ProjectilePoolSize; ++i)
        {
            GameObject new_proj = GameObject.Instantiate(WeapData.ProjectilePrefab);

            Projectile script = new_proj.GetComponent<Projectile>();

            script.Init(this);

            Projectiles.Add(script);
        }
    }

    public void SetGraphics()
    {
        if(WeaponRenderer != null)
            WeaponRenderer.sprite = WeapData.Graphics;

        FireCooldown = WeapData.FireCooldown;
        ProjectileTTL = WeapData.ProjectileTTL;
        ProjectileSpeed = WeapData.ProjectileSpeed;
    }

#if UNITY_EDITOR

    public void SaveGraphics()
    {
        if (WeaponRenderer != null)
            WeapData.Graphics = WeaponRenderer.sprite;

        WeapData.FireCooldown = FireCooldown;
        WeapData.ProjectileTTL = ProjectileTTL;
        WeapData.ProjectileSpeed = ProjectileSpeed;

        EditorUtility.SetDirty(WeapData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

#endif

    public void Fire()
    {
        if (Time.time - LastShootTime < WeapData.FireCooldown)
            return;

        foreach(var projectile in Projectiles)
        {
            if (projectile.gameObject.activeSelf)
                continue;

            projectile.Fire();
            LastShootTime = Time.time;

            break;
        }
    }
}
