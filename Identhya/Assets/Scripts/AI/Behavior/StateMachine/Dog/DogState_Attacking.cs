using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Attack
{
    public string AnimKey;
    public float Distance;
    public float Strength;
    public GameObject EffectPrefab;
}

public class DogState_Attacking : DogState
{
    Coroutine attack;
    Coroutine keepdistance;
    Coroutine orienttotarget;
    [SerializeField] GameObject target;

    [SerializeField] Transform Probe_N;

    [SerializeField] Transform EffectTransform;

    public bool _checkStopped = false;

    [SerializeField] Attack[] attacks;

    public float MaxAttackDistance = 1.0f;

    public float TargetLostTime = 2.0f;

    [SerializeField] float CombatDistance = 10.0f;
    [SerializeField] float AttackDistance = 0.8f;

    Coroutine targetLostCoro = null;

    public override void OnStateEnter(State prevState)
    {
        target = GetComponent<Sight>().SeekingObject_N;
        navmeshAgent.isStopped = false;
        navmeshAgent.speed = 10.0f;
        keepdistance = StartCoroutine(KeepDistance(CombatDistance));
        orienttotarget = StartCoroutine(OrientToTarget());
        GetComponent<Sight>().OnObjectLost += OnTargetLost;
        GetComponent<Sight>().OnObjectSighted += OnObjectSighted;
    }

    public override void OnStateLeave(State nextState)
    {
        Debug.Log("Exitting attack state!");
        StopAllCoroutines();
        GetComponent<Sight>().OnObjectLost -= OnTargetLost;
        GetComponent<Sight>().OnObjectSighted -= OnObjectSighted;
    }

    public override void UpdateState()
    {
        if (targetLostCoro == null)
        {
            FindObjectOfType<AttackMusicController>().PlayerUnderAttack();
        }
    }

    private void OnTargetLost(GameObject target, GameObject tracker)
    {
        Debug.Log(" OBJECT LOST !!!!!!!!!!!!!!!!!!!  ");
        if (targetLostCoro == null)
        {
            targetLostCoro = StartCoroutine(TargetLost(TargetLostTime));
        }
    }

    private void OnObjectSighted(GameObject target, GameObject tracker)
    {
        if(targetLostCoro != null)
        {
            StopCoroutine(targetLostCoro);
            targetLostCoro = null;
        }
    }

    IEnumerator TargetLost(float delay)
    {
        yield return new WaitForSeconds(delay);
        machine.SetState(GetComponent<DogState_Idle>());
    }

    /*IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();
    }*/

    IEnumerator OrientToTarget()
    {
        for(; ;)
        {
            dog.transform.LookAt(target.transform);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator KeepDistance(float Dist)
    {
        for (; ; )
        {
            //Debug.Log(" ----------------  " + DistanceToTarget());
            if (DistanceToTarget() > Dist)
            {
                navmeshAgent.speed = dog.PeltSpeed;
                navmeshAgent.isStopped = false;
                _checkStopped = false;
                navmeshAgent.destination = target.transform.position;
                yield return new WaitForSeconds(0.05f);
                while (navmeshAgent.remainingDistance > Dist)
                {
                    navmeshAgent.destination = target.transform.position;
                    yield return new WaitForSeconds(0.05f);
                }
            }
            else
            {
                navmeshAgent.isStopped = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                _checkStopped = true;
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(PerformAttack());
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private float DistanceToTarget()
    {
        return (target.transform.position - dog.transform.position).magnitude;
    }

    private IEnumerator PerformAttack()
    {
        navmeshAgent.isStopped = false;
        _checkStopped = false;
        int AttackIndex = Random.Range(0, attacks.Length);
        float prevSpeed = navmeshAgent.speed;
        navmeshAgent.speed = dog.AttackSpeed;
        navmeshAgent.destination = target.transform.position;
        yield return new WaitForSeconds(0.15f);
        while (navmeshAgent.remainingDistance > (attacks[AttackIndex].Distance + 0.05f))
        {
            navmeshAgent.destination = target.transform.position;
            yield return new WaitForSeconds(0.05f);
        }
        navmeshAgent.isStopped = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _checkStopped = true;

        dog.animator.SetBool(attacks[AttackIndex].AnimKey, true);
        if(attacks[AttackIndex].EffectPrefab!=null)
        {
            GameObject newEffect = (GameObject)Instantiate(attacks[AttackIndex].EffectPrefab);
            newEffect.transform.SetParent(EffectTransform);
            newEffect.transform.localScale = Vector3.one;
            newEffect.transform.localPosition = Vector3.zero;
            newEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        yield return new WaitForSeconds(0.8f);

        if ((target.transform.position - dog.transform.position).magnitude < attacks[AttackIndex].Distance)
        {
            target.GetComponent<HealthComponent>().TakeDamage(attacks[AttackIndex].Strength);
        }
        yield return new WaitForSeconds(0.8f);

        dog.animator.SetBool(attacks[AttackIndex].AnimKey, false);
        yield return new WaitForSeconds(0.15f);

        navmeshAgent.speed = prevSpeed;
        if ((target.transform.position - dog.transform.position).magnitude < 5.0f)
        {
            yield return StartCoroutine(MoveBackwardFromTarget(CombatDistance));
        }
    }

    private IEnumerator MoveBackwardFromTarget(float Distance)
    {
        navmeshAgent.isStopped = false;
        _checkStopped = false;
        navmeshAgent.speed = dog.WalkSpeed;
        navmeshAgent.updateRotation = false;
        Vector3 Destination = target.transform.position - (target.transform.position - dog.transform.position).normalized * Distance;
        navmeshAgent.destination = Destination;
        if(Probe_N != null)
        {
            Probe_N.position = Destination;
        }
        dog.SpeedAnimationMultiplier = -5.0f;
        float elapsed = 0.0f;
        const float maxElapsed = 1.5f;
        while ((elapsed < maxElapsed) && (target.transform.position - dog.transform.position).magnitude < (Distance-0.75f))
        {
            navmeshAgent.destination = Destination;
            yield return new WaitForSeconds(0.05f);
            elapsed += 0.05f;
            //Debug.Log(" ==============================   MOVE BACKWARD ================================ " + (target.transform.position - dog.transform.position).magnitude);
        }
        if(Probe_N != null)
        {
            Probe_N.position = Vector3.zero;
        }
        dog.SpeedAnimationMultiplier = 1.0f;
        navmeshAgent.updateRotation = true;
        navmeshAgent.isStopped = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _checkStopped = true;
        yield return new WaitForSeconds(0.15f);
    }
}
