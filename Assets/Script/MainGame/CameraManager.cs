using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform Character;
    public Camera MainCamera;
    public Vector2 SizeCheck;
    public float MinSize;

    Collider2D[] HitsBuffer = new Collider2D[10];

    float OriginalZ = 0.0f;

    float OriginalOrthoSize = 0.0f;

    public int CollisionLayer;

    public float CameraSizeSpeed = 5.0f;
    public float TargetOrthoSize;

    private void Start()
    {
        OriginalZ = MainCamera.transform.position.z;
        OriginalOrthoSize = MainCamera.orthographicSize;
        TargetOrthoSize = OriginalOrthoSize;
        CollisionLayer = 1 << LayerMask.NameToLayer("Character");
    }

    void LateUpdate()
    {
        HandleSizeUpdate();
    }

    private void HandleSizeUpdate()
    {
        MainCamera.transform.position = new Vector3(Character.position.x, Character.position.y, OriginalZ);

        int n_hit = Physics2D.OverlapBoxNonAlloc(transform.position, SizeCheck, 0.0f, HitsBuffer, CollisionLayer);

        if (n_hit > 1)
        {
            float max_distance = -1.0f;
            Vector2 max_half_size = Vector2.zero;

            for (int i = 0; i < n_hit; ++i)
            {
                float distance = Vector2.Distance(Character.position, HitsBuffer[i].transform.position);

                if (distance > max_distance)
                {
                    max_distance = distance;

                    float x = Mathf.Abs(Character.position.x - HitsBuffer[i].transform.position.x);
                    float y = Mathf.Abs(Character.position.y - HitsBuffer[i].transform.position.y);
                    max_half_size = new Vector2(x, y);
                }
            }

            Vector2 size = max_half_size * 2.5f;

            float new_ortho_size = CalculateOrtographicSize(size);

            TargetOrthoSize = new_ortho_size < MinSize ? MinSize : new_ortho_size;
        }
        else
        {
            TargetOrthoSize = OriginalOrthoSize;
        }

        MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, TargetOrthoSize, Time.deltaTime * CameraSizeSpeed);
    }

    private float CalculateOrtographicSize(Vector2 size)
    {
        float aspect = Screen.width / (float)Screen.height;

        float height = size.x / aspect;

        if (height < size.y)
            height = size.y;

        return height / 2;
    }
}
