using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform Character;
    public Camera MainCamera;

    float OriginalZ = 0.0f;

    private void Start()
    {
        OriginalZ = MainCamera.transform.position.z;
    }

    void LateUpdate()
    {
        MainCamera.transform.position = new Vector3(Character.position.x, Character.position.y, OriginalZ);
    }
}
