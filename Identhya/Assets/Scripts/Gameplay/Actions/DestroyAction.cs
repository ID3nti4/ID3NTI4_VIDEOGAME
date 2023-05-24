using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAction : GameplayAction
{
    public GameObject ObjectToDestroy;
    public override Coroutine DoAction(GameObject source)
    {
        Destroy(ObjectToDestroy, Time.deltaTime*2.0f);
        return StartCoroutine(FinishImmediately());
    }
}
