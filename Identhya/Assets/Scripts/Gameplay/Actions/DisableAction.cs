using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAction : GameplayAction
{

    public GameObject ObjectToDisable;

    public override Coroutine DoAction(GameObject source)
    {
        ObjectToDisable.SetActive(false);
        return StartCoroutine(FinishImmediately());
    }
}
