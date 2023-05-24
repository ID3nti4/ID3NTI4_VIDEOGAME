using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConditionAction : GameplayAction
{
    public CheckInventoryCondition condition;
    public GameplayAction action;
    public override Coroutine DoAction(GameObject source)
    {
        if (condition.CheckCondition(source))
        {
            return action.DoAction(source);
        }
        else
        {
            return StartCoroutine(FinishImmediately());
        }
    }
}
