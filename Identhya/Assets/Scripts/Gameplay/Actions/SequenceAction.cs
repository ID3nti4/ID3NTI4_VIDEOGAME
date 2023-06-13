using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAction : GameplayAction
{
    public GameplayAction[] Actions;

    public override Coroutine DoAction(GameObject source)
    {
        return StartCoroutine(DoAllActions(source));
    }

    IEnumerator DoAllActions(GameObject source)
    {
        int index = 0;
        while(index < Actions.Length)
        {
            Debug.Log("Did action " + index);
            Actions[index].DoAction(source);
            yield return null;
            index++;
        }
    }
}
