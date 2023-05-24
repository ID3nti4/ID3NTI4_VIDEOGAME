using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAction : GameplayAction
{
    public GameObject ObjectToEnable;

    public override Coroutine DoAction(GameObject source)
    {
        ObjectToEnable.SetActive(true);
        return StartCoroutine(FinishImmediately());
    }
}
