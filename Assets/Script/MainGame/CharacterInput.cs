using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    public Character CharacterRef { get; internal set; }

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

        CharacterRef.RotateTowards(Input.mousePosition);

        if (Input.GetButton("Fire1"))
            CharacterRef.Fire();
    }
}
