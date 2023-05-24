using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState_Idle : DogState
{
    [SerializeField] float MinTimeToWait = 2.0f;
    [SerializeField] float MaxTimeToWait = 5.0f;

    public override void OnStateEnter(State prevState)
    {
        float TimeToWait = Random.Range(MinTimeToWait, MaxTimeToWait);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        navmeshAgent.speed = 6.0f;
        navmeshAgent.isStopped = true;
        StartCoroutine(Wait(TimeToWait));
    }

    IEnumerator Wait(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        WaitEnded();
    }

    private void WaitEnded()
    {
        machine.SetState(GetComponent<DogState_Loitering>());
    }
}
