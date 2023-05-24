using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIFader : MonoBehaviour
{
    RawImage rawImage;

    public float OpacitySpeed = 1.0f;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    public float CurrentOpacity = 1.0f;

    public void FadeToOpacityImmediately(float op)
    {
        rawImage.color = new Color(0, 0, 0, op);
        CurrentOpacity = op;
    }

    public Coroutine FadeToOpaque()
    {
        return StartCoroutine(FadeToOpacity(1.0f));
    }

    public Coroutine FadeToTransparent()
    {
        return StartCoroutine(FadeToOpacity(0.0f));
    }

    IEnumerator FadeToOpacity(float op)
    {
        while(CurrentOpacity != op)
        {
            if(CurrentOpacity < op)
            {
                CurrentOpacity = Mathf.Min(CurrentOpacity + OpacitySpeed * Time.deltaTime, op);
            }
            else
            {
                CurrentOpacity = Mathf.Max(CurrentOpacity - OpacitySpeed * Time.deltaTime, op);
            }

            rawImage.color = new Color(0, 0, 0, CurrentOpacity);

            yield return new WaitForEndOfFrame();
        }
    }
}
