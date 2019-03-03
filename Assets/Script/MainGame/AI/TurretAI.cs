using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : GeneralAI
{
    protected override void UpdateState()
    {
        if (Vector2.Distance(Player.transform.position, transform.position) < ViewRange)
            State = AIState.FIRING;
        else
            State = AIState.WAITING;

        switch (State)
        {
            case AIState.WAITING:



                break;
            case AIState.FIRING:

                RotateTowards(Player.transform.position);

                CharacterRef.Fire();

                break;
        }
    }

    public void Fire()
    {
        CharacterRef.Fire();
    }
}
