using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentAction : GameplayAction
{
    public GameObject ObjectToParent;
    public GameObject NewParent;
    public bool ZeroPosition = true;
    public override Coroutine DoAction(GameObject source)
    {
        ObjectToParent.transform.SetParent(NewParent.transform);
        if (ZeroPosition) 
        {
            ObjectToParent.transform.localPosition = Vector3.zero;
        }

        return StartCoroutine(FinishImmediately());
    }
}
