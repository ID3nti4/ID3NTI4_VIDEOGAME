using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDroneSoundAction : GameplayAction
{
    public AudioClip clip;

    DroneFXController fxController;

    private void Start()
    {
        fxController = GetComponent<DroneFXController>();
    }

    public override Coroutine DoAction(GameObject source)
    {
        if (fxController != null)
        {
            if (clip != null)
            {
                fxController.Play(clip);
            }
        }
        return StartCoroutine(FinishImmediately());
    }
}
