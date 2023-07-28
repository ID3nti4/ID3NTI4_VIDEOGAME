using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionOnDeath1 : MonoBehaviour
{
    public GameplayAction action;

    void Start()
    {
        
    }

    private void OnDestroy()
    {
        StartCoroutine(PerformAction());
    }

    IEnumerator PerformAction()
    {
        yield return action.DoAction(this.gameObject);
    }
}
