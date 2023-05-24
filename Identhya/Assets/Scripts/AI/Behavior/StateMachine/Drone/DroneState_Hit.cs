using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneState_Hit : DroneState
{
    public float HitTime = 3.5f;
    public float HitTimeRange = 0.5f;
    public GameObject ElectricityPrefab;

    public override void OnStateEnter(State prevState)
    {
        GetComponent<Sight>().Enabled = false;
        StartCoroutine(RandomRotations());    
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
        Vector3 HitPosition = drone.transform.position;
        float elapsed = 0.0f;
        while(elapsed < hitTime)
        {
            Vector3 NewPosition = HitPosition;
            float miniWait = 0.25f;
            while (miniWait > 0.0f)
            {
                NewPosition = HitPosition + Random.onUnitSphere * 0.08f;
                drone.transform.position = NewPosition;
                miniWait -= Time.deltaTime;
                elapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            GameObject newGO = (GameObject)Instantiate(ElectricityPrefab);
             
            newGO.transform.position = NewPosition;
            
        }
        drone.transform.position = HitPosition;
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<StunComponent>().OnStunEnded?.Invoke();
    }
}
