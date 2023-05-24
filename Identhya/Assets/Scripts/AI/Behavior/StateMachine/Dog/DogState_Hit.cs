using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState_Hit : DogState
{
    public float HitTime = 0.5f;
    public float HitTimeRange = 0.15f;
    public GameObject ElectricityPrefab;
    DogState prevState;

    public override void OnStateEnter(State prevState)
    {
        if (prevState != this)
        {
            Debug.Log(" ##########  Entering HIT state from " + prevState.StateName);
            this.prevState = (DogState)prevState;
            GetComponent<Sight>().Enabled = false;
            StartCoroutine(RandomRotations());
        }
    }

    public override void OnStateLeave(State nextState)
    {
        GetComponent<Sight>().Enabled = true;
    }

    public override void UpdateState()
    {

    }

    IEnumerator RandomRotations()
    {
        float hitTime = Random.Range(HitTime - HitTimeRange, HitTime + HitTimeRange);
        Vector3 HitPosition = dog.transform.position;
        float elapsed = 0.0f;
        while (elapsed < hitTime)
        {
            Vector3 NewPosition = HitPosition;

            NewPosition = HitPosition + Random.onUnitSphere * 0.12f;
            dog.transform.position = NewPosition;
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();


        }
        dog.transform.position = HitPosition;
        yield return new WaitForSeconds(0.5f);
        machine.SetState<DogState>(prevState);
    }
}
