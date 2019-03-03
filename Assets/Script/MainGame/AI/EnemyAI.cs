using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : GeneralAI
{
    Humanoid HumanRef;

    protected new void Start()
    {
        base.Start();

        HumanRef = CharacterRef as Humanoid;
    }

    protected override void UpdateState()
    {
        float distance = Vector2.Distance(Player.transform.position, transform.position);

        if (distance < FireRange)
            State = AIState.FIRING;
        else if(distance < ViewRange)
            State = AIState.WALKING;
        else
            State = AIState.WAITING;

        switch (State)
        {
            case AIState.WAITING:



                break;
            case AIState.WALKING:

                RotateTowards(Player.transform.position);

                Vector2 dir = (Player.transform.position - transform.position).normalized;

                HumanRef.Move(dir.x, dir.y);

                break;
            case AIState.FIRING:

                RotateTowards(Player.transform.position);

                HumanRef.Fire();

                break;
        }
    }
}
