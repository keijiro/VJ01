using UnityEngine;
using System.Collections;

public class SceneTransitionWithFading : MonoBehaviour
{
    public Color fadeInColor;
    public Color fadeOutColor;
    public float duration = 1.5f;
    public string nextLevel;

    Material material;
    float opacity;
    float delta;
    int skipFrame;
    bool changeScene;

    public void BeginFadeOut (string level)
    {
        if (level != null) nextLevel = level;
        delta = 1.0f / duration;
        enabled = true;
        material.color = fadeOutColor;
    }

    void Awake ()
    {
        material = new Material (Shader.Find ("Hidden/FadeToColor"));
        material.color = fadeInColor;
        opacity = 1.0f;
        delta = -1.0f / duration;
        skipFrame = 4;
    }

    void Update ()
    {
        if (changeScene)
        {
            if (nextLevel != null)
            {
                Application.LoadLevel(nextLevel);
                nextLevel = null;
            }
        }
        else if (skipFrame > 0)
        {
            skipFrame--;
        }
        else
        {
            opacity = Mathf.Clamp01 (opacity + delta * Time.deltaTime);
            if (opacity == 0.0f) enabled = false;
            if (opacity == 1.0f) changeScene = true;
        }
    }

    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        material.SetFloat ("_Opacity", opacity);
        Graphics.Blit (source, destination, material);
    }
}
