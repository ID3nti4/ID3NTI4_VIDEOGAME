using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionOnDamageBlocked : MonoBehaviour
{
    public GameplayAction action;
    
    void Start()
    {
        GetComponent<HealthComponent>().OnCharacterBlockedDamage += OnBlockedDamage;
    }

    private void OnBlockedDamage(GameObject Source)
    {
        StartCoroutine(PerformAction());
    }

    IEnumerator PerformAction()
    {
        yield return action.DoAction(this.gameObject);
    }

    
}
