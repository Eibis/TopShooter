using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Weapon Weapon;

    public float ViewRange = 20.0f;
    public float RotationSpeed = 5.0f;
    public TurretState State = TurretState.WAITING;

    Character Player;

    public enum TurretState
    {
        WAITING,
        FIRING
    }

    void Start()
    {
        Player = GameManager.Instance.Player;
    }

    void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (Vector2.Distance(Player.transform.position, transform.position) < ViewRange)
            State = TurretState.FIRING;
        else
            State = TurretState.WAITING;

        switch(State)
        {
            case TurretState.WAITING:



                break;
            case TurretState.FIRING:

                RotateTowards(Player.transform.position);

                Weapon.Fire();

                break;
        }
    }

    internal void RotateTowards(Vector2 target_position)
    {
        target_position -= (Vector2)transform.position;

        var angle_radians = Mathf.Atan2(target_position.y, target_position.x);

        Quaternion target_rotation = Quaternion.Euler(0.0f, 0.0f, angle_radians * Mathf.Rad2Deg - 90.0f);

        transform.rotation = Quaternion.Lerp(transform.rotation, target_rotation, Time.deltaTime * RotationSpeed);
    }

    public void Fire()
    {
        Weapon.Fire();
    }
}
