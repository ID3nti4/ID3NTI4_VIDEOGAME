using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavernSkipTimeline : GameplayAction
{
    public GameObject faderCanvas;
    public GameObject restOfCamera;

    public override Coroutine DoAction(GameObject source)
    {
        faderCanvas.SetActive(false);
        restOfCamera.SetActive(false);
        return StartCoroutine(FinishImmediately());
    }
}
