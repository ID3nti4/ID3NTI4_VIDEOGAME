using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionOnDamage : MonoBehaviour
{
    public GameplayAction action;

    void Start()
    {
        GetComponent<HealthComponent>().OnCharacterDamage += OnDamage;
    }

    private void OnDamage(float Amount, AttackModifier Modifier, GameObject Source)
    {
            StartCoroutine(PerformAction());
    }

    IEnumerator PerformAction()
    {
        yield return action.DoAction(this.gameObject);
    }
}
