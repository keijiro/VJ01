using UnityEngine;
using System.Collections;

public class TextureExchanger : MonoBehaviour
{
    public Texture[] textures;

    void Start ()
    {
        renderer.material.mainTexture = textures[Random.Range (0, textures.Length)];
    }
}
