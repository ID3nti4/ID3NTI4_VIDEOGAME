using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLookAroundComponent : MonoBehaviour
{
    StateMachine stateMachine_A;

    Coroutine lookAround = null;

    Drone drone;

    public Transform lookAroundTransform;

    Vector3 lookAroundEulers;

    float TargetPitch = 0.0f;
    float TargetYaw = 0.0f;
    float CurrentPitch = 0.0f;
    float CurrentYaw = 0.0f;

    public float LerpFraction = 0.08f;

    private void Start()
    {
        drone = GetComponent<Drone>();
        if(stateMachine_A == null)
        {
            stateMachine_A = GetComponentInChildren<StateMachine>();
            if(stateMachine_A == null)
            {
                stateMachine_A = GetComponentInParent<StateMachine>();
            }
        }

        if(stateMachine_A!=null)
        {
            stateMachine_A.OnStateChanged += OnStateChanged;
        }

        lookAroundEulers = lookAroundTransform.localRotation.eulerAngles;

    }

    private void UpdateSoftVariables()
    {
        CurrentPitch = Mathf.Lerp(CurrentPitch, TargetPitch, LerpFraction * Time.deltaTime);
        CurrentYaw = Mathf.Lerp(CurrentYaw, TargetYaw, LerpFraction * Time.deltaTime);
    }

    private void OnStateChanged(string StateName, string StateClass)
    {
       
        if(StateClass != "Hostile")
        {
            StartLookAround();
            StopLookAt();
        }
        else
        {
            StopLookAround();
            StartLookAt();
        }
    }

    private void StartLookAround()
    {
        if(lookAround==null)
        {
            lookAround = StartCoroutine(LookAround());
        }
    }

    public void StopLookAround()
    {
        if(lookAround != null)
        {
            StopCoroutine(lookAround);
            lookAround = null;
        }
    }

    bool lookingAt = false;

    private void StartLookAt()
    {
        lookingAt = true;
    }

    private void StopLookAt()
    {
        lookingAt = false;
    }

    IEnumerator LookAround()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
            if (Utils.TryProbability(0.75f))
            {
                TargetYaw = 45.0f;
                yield return new WaitForSeconds(3.0f);
                TargetYaw = -45.0f;
                yield return new WaitForSeconds(3.0f);
                TargetYaw = 0.0f;
            }
            else
            {
                TargetPitch = 45.0f;
                yield return new WaitForSeconds(3.0f);
                TargetPitch = -45.0f;
                yield return new WaitForSeconds(3.0f);
                TargetPitch = 0.0f;
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void Update()
    {
        
        if(lookingAt)
        {
            if (drone.Target != null)
            {
                Quaternion q = Quaternion.LookRotation(drone.Target.transform.position - lookAroundTransform.position);
                Vector3 Eulers = q.eulerAngles;

                lookAroundTransform.rotation = q;
            }
            
        }
        else
        {
            UpdateSoftVariables();
            Vector3 newEuler = new Vector3(lookAroundEulers.x + CurrentPitch, CurrentYaw, 0.0f);
            lookAroundTransform.localEulerAngles = newEuler;
        }
    }

}
