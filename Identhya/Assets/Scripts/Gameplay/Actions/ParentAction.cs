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
        Debug.Log("empìeza Element4");
        ObjectToParent.transform.SetParent(NewParent.transform);
        if (ZeroPosition) 
        {
            ObjectToParent.transform.localPosition = Vector3.zero;
        }

        Debug.Log("termina Element4");
        return StartCoroutine(FinishImmediately());
    }
}
