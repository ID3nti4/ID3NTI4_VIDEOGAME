using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayAction : MonoBehaviour
{
    public abstract Coroutine DoAction(GameObject source);

    protected IEnumerator FinishImmediately()
    {
        yield return new WaitForEndOfFrame();
    }
}
