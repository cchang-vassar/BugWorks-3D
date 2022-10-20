using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightCheckScript : MonoBehaviour
{
    public RenderTexture lightCheck;
    public float lightLevel;
    public new int light;
    public float lightCoefficient = 0.4f;

    void Update()
    {
        RenderTexture tempTexture = RenderTexture.GetTemporary(lightCheck.width, lightCheck.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(lightCheck, tempTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tempTexture;

        Texture2D temp2DTexture = new Texture2D(lightCheck.width, lightCheck.height);
        temp2DTexture.ReadPixels(new Rect(0, 0, tempTexture.width, tempTexture.height), 0, 0);
        temp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tempTexture);

        Color32[] colors = temp2DTexture.GetPixels32();

        lightLevel = 0;
        for (int i = 0; i < colors.Length; i++)
        {
            lightLevel += (0.2126f * colors[i].r) + (0.7152f * colors[i].g) + (0.0722f * colors[i].b);
        }

        lightLevel -= 1117234;
        lightLevel = lightLevel / colors.Length;
        light = Mathf.RoundToInt(lightLevel);
    }
}