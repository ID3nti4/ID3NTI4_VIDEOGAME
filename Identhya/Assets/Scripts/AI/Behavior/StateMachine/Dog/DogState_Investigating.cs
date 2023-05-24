using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState_Investigating : DogState
{
    Coroutine investigating;
    public Vector3 soundLocation;
    bool restartcoro = false;
    public override void OnStateEnter(State prevState)
    {
        GetComponent<Hearing>().OnObjectHeard += OnObjectHeard;
        GetComponent<Sight>().OnObjectSighted += OnTargetSighted;
        investigating = StartCoroutine(Investigating());
    }

    private void OnObjectHeard(GameObject target, GameObject hearer)
    {
        soundLocation = target.transform.position;
        navmeshAgent.destination = soundLocation;
        if(restartcoro)
        {
            StopCoroutine(investigating);
            investigating = StartCoroutine(Investigating());
        }
    }

    private void OnTargetSighted(GameObject target, GameObject sighter)
    {
        machine.SetState(GetComponent<DogState_Engaging>());
    }

    public override void OnStateLeave(State nextState)
    {
        GetComponent<Hearing>().OnObjectHeard -= OnObjectHeard;
        GetComponent<Sight>().OnObjectSighted -= OnTargetSighted;
        StopCoroutine(investigating);
    }

    IEnumerator Investigating()
    {
        restartcoro = false;
        navmeshAgent.speed = 2.5f;
        navmeshAgent.isStopped = false;
        navmeshAgent.destination = soundLocation;
        while(navmeshAgent.remainingDistance > 1.0f)
        {
            navmeshAgent.destination = soundLocation;
            yield return new WaitForSeconds(0.1f);
        }
        restartcoro = true;
        yield return new WaitForSeconds(3.0f);
        machine.SetState(GetComponent<DogState_Loitering>());
    }
}
