using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform Character;
    public Camera MainCamera;
    public Vector2 SizeCheck;

    Collider2D[] HitsBuffer = new Collider2D[10];

    float OriginalZ = 0.0f;

    float OriginalOrthoSize = 0.0f;

    private void Start()
    {
        OriginalZ = MainCamera.transform.position.z;
        OriginalOrthoSize = MainCamera.orthographicSize;
    }

    void LateUpdate()
    {
        MainCamera.transform.position = new Vector3(Character.position.x, Character.position.y, OriginalZ);

        int n_hit = Physics2D.OverlapBoxNonAlloc(transform.position, SizeCheck, 0.0f, HitsBuffer, LayerMask.NameToLayer("Character"));

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

            MainCamera.orthographicSize = CalculateOrtographicSize(max_half_size);
        }
        else
        {
            MainCamera.orthographicSize = OriginalOrthoSize;
        }
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
