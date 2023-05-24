using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionOnDeath : MonoBehaviour
{
    public GameplayAction action;

    void Start()
    {
        GetComponent<HealthComponent>().OnCharacterDied += OnDeath;
    }

    private void OnDeath(GameObject Source)
    {
        StartCoroutine(PerformAction());
    }

    IEnumerator PerformAction()
    {
        yield return action.DoAction(this.gameObject);
    }
}
