using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFadeIn : MonoBehaviour
{
    public UIFader fader;
    void Start()
    {
        fader.FadeToTransparent();
    }

}
