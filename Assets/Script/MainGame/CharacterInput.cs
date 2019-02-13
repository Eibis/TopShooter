using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public Animator Anim;

    Vector3 MovementVec;

    public Character CharacterRef { get; internal set; }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        float hor_inp = Input.GetAxis("Horizontal");
        float ver_inp = Input.GetAxis("Vertical");

        MovementVec = new Vector3(0.0f, 0.0f, 0.0f);

        if (hor_inp != 0.0f)
        {
            MovementVec += new Vector3(Time.deltaTime * CharacterRef.Speed * hor_inp, 0.0f, 0.0f);
        }

        if (ver_inp != 0.0f)
        {
            MovementVec += new Vector3(0.0f, Time.deltaTime * CharacterRef.Speed * ver_inp, 0.0f);
        }

        transform.position += MovementVec;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angleRadians = Mathf.Atan2(mousePosition.y, mousePosition.x);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angleRadians * Mathf.Rad2Deg - 90.0f);

        Anim.SetFloat("Speed", MovementVec.magnitude);
    }
}
