using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionOnStun : MonoBehaviour
{
    public GameplayAction action;

    void Start()
    {
        GetComponent<StunComponent>().OnStunStarted += OnStun;
    }

    private void OnStun()
    {
        StartCoroutine(PerformAction());
    }

    IEnumerator PerformAction()
    {
        yield return action.DoAction(this.gameObject);
    }
}

