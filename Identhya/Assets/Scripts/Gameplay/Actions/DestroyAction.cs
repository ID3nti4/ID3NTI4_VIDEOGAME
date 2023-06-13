using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAction : GameplayAction
{
    public GameObject ObjectToDestroy;
    public override Coroutine DoAction(GameObject source)
    {
        Debug.Log("empìeza Element1");
        Destroy(ObjectToDestroy, Time.deltaTime*2.0f);
        Debug.Log("termina Element1");
        return StartCoroutine(FinishImmediately());
    }
}
