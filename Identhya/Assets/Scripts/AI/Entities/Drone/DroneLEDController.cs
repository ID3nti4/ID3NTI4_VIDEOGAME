using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLEDController : DroneComponent
{
    public Renderer LEDRenderer;
    public new Light light;
    Material material;
    public SpriteRenderer MapIconRenderer;

    [ColorUsageAttribute(true, true)]
    public Color IdleColor;
    [ColorUsageAttribute(true, true)]
    public Color AlertedColor;
    [ColorUsageAttribute(true, true)]
    public Color PursuitColor;

    Coroutine currentColorChange;

    void Awake()
    {
        material = LEDRenderer.material;
        MapIconRenderer.color = IdleColor;
        currentColorChange = null;
    }

    public void ChangeColor(Color newColor)
    {
        if(currentColorChange != null)
        {
            StopCoroutine(currentColorChange);
        }
        currentColorChange = StartCoroutine(ChangeColorCoro(newColor));
    }

    public override void Reset()
    {
        if(currentColorChange != null)
        {
            StopCoroutine(currentColorChange);
        }
        material.SetColor("Color_6A6BE5C2", IdleColor);
        MapIconRenderer.color = IdleColor;
        light.color = IdleColor;
    }

    IEnumerator ChangeColorCoro(Color newColor)
    {
        Color oldcolor = material.GetColor("Color_6A6BE5C2");
        MapIconRenderer.color = newColor;

        for (int i = 0; i < 3; ++i)
        {
            yield return new WaitForSeconds(0.15f);
            material.SetColor("Color_6A6BE5C2", Color.black);
            light.color = Color.black;
            yield return new WaitForSeconds(0.15f);
            material.SetColor("Color_6A6BE5C2", oldcolor);
            light.color = oldcolor;
        }

        material.SetColor("Color_6A6BE5C2", Color.black);
        light.color = Color.black;
        yield return new WaitForSeconds(0.25f);

        material.SetColor("Color_6A6BE5C2", newColor);
        light.color = newColor;
    }
    
}
