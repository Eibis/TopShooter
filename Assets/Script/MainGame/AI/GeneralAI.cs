using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAI : MonoBehaviour
{
    public Weapon Weapon;

    public float ViewRange = 20.0f;
    public float FireRange = 10.0f;

    public float RotationSpeed = 5.0f;
    public AIState State = AIState.WAITING;

    protected Character Player;

    public enum AIState
    {
        WAITING,
        WALKING,
        FIRING
    }

    protected void Start()
    {
        Player = GameManager.Instance.Player;
    }

    protected void Update()
    {
        UpdateState();
    }

    protected virtual void UpdateState() {}

    internal void RotateTowards(Vector2 target_position)
    {
        target_position -= (Vector2)transform.position;

        var angle_radians = Mathf.Atan2(target_position.y, target_position.x);

        Quaternion target_rotation = Quaternion.Euler(0.0f, 0.0f, angle_radians * Mathf.Rad2Deg - 90.0f);

        transform.rotation = Quaternion.Lerp(transform.rotation, target_rotation, Time.deltaTime * RotationSpeed);
    }
}
