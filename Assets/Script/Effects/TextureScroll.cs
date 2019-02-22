using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public Renderer Renderer;

    Material ScrollableMaterial;
    float CurrentScroll = 0.0f;
    float ScrollSpeed = 5.0f;

    void Start()
    {
        ScrollableMaterial = Renderer.material;

    }

    void Update()
    {
        CurrentScroll += ScrollSpeed * Time.deltaTime;
        foreach (var texture_name in ScrollableMaterial.GetTexturePropertyNames())
        {
            ScrollableMaterial.SetTextureOffset(texture_name, new Vector2(0, -CurrentScroll));
            Debug.Log(texture_name);
        }
        //https://answers.unity.com/questions/605905/dynamic-offset-tiling-for-spriterenderer.html
        //https://forum.unity.com/threads/is-it-still-possible-to-scroll-sprite-textures-with-material-maintextureoffset.451947/

    }
}
