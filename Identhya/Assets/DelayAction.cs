using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAction : GameplayAction
{
    public float Delay = 1.0f;
    public override Coroutine DoAction(GameObject source)
    {
        return StartCoroutine(DelayCoRo(Delay));
    }

    IEnumerator DelayCoRo(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
