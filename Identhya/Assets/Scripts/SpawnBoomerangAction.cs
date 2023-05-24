using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoomerangAction : GameplayAction
{
    public override Coroutine DoAction(GameObject source)
    {
        FindObjectOfType<BoomerangController>().SpawnBoomerang();
        return StartCoroutine(FinishImmediately());
    }
}
