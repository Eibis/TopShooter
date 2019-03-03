using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public Humanoid CharacterRef;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        float hor_inp = Input.GetAxis("Horizontal");
        float ver_inp = Input.GetAxis("Vertical");

        if (hor_inp != 0.0f || ver_inp != 0.0f)
            CharacterRef.Move(hor_inp, ver_inp);

        RotateTowards(Input.mousePosition);

        if (Input.GetButton("Fire1"))
            CharacterRef.Fire();
    }

    internal void RotateTowards(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
        var angle_radians = Mathf.Atan2(mousePosition.y, mousePosition.x);

        Quaternion target_rotation = Quaternion.Euler(0.0f, 0.0f, angle_radians * Mathf.Rad2Deg - 90.0f);

        CharacterRef.Rotate(Quaternion.Lerp(transform.rotation, target_rotation, Time.deltaTime * CharacterRef.RotationSpeed));
    }
}
