using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogState_Engaging : DogState
{
    NavMeshAgent agent;

    Coroutine engage;
    public override void OnStateEnter(State prevState)
    {
        agent = GetComponent<NavMeshAgent>();
        engage = StartCoroutine(Engage());
    }

    public override void OnStateLeave(State nextState)
    {
        StopCoroutine(engage);
    }

    public override void UpdateState()
    {
        FindObjectOfType<AttackMusicController>().PlayerUnderAttack();
    }

    IEnumerator Engage()
    {
        GameObject enemy = GetComponent<Sight>().SeekingObject_N;
        Vector3 DirectionToEnemy = (enemy.transform.position - dog.transform.position).normalized;
        agent.destination = dog.transform.position + DirectionToEnemy * 0.2f;
        yield return new WaitForSeconds(0.4f);
        agent.speed = dog.PeltSpeed;
        agent.destination = enemy.transform.position;
        yield return new WaitForSeconds(0.05f);
        //Debug.Log(" >>>>>>>>>>>>>>> " + agent.remainingDistance);
        while (agent.remainingDistance > 10.5f)
        {
            
            yield return new WaitForSeconds(0.05f);
            agent.SetDestination(enemy.transform.position);
            //Debug.Log(" >>>>>>>>>>>>>>> " + agent.remainingDistance);
        }
        agent.speed = dog.WalkSpeed;
        agent.isStopped = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        machine.SetState(GetComponent<DogState_Attacking>());
    }
}
