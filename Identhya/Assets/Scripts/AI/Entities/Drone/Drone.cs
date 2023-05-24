using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public static float WideVisionAngle = 175.0f;
    public static float MiddleVisionAngle = 85.0f;
    public static float NarrowVisionAngle = 40.0f;

    public GameObject Target;

    private void Start()
    {
        Sight sight = GetComponent<Sight>();
        sight.OnObjectSighted += SetTarget;
        sight.OnObjectLost += ClearTarget;
        StunComponent stun = GetComponent<StunComponent>();
        stun.OnStunStarted += OnStunned;
        stun.OnStunEnded += OnStunEnded;
    }

    private void SetTarget(GameObject sighted, GameObject Sighter)
    {
        Target = sighted;
    }

    private void ClearTarget(GameObject sighted, GameObject Sighter)
    {
        Target = null;
    }

    DroneState StunPreviousState;
    private void OnStunned()
    {
        StateMachine stateMachine = GetComponent<StateMachine>();
        GetComponent<DroneLookAroundComponent>().StopLookAround();
        StunPreviousState = (DroneState)stateMachine.GetState();
        stateMachine.SetState<DroneState>(GetComponent<DroneState_Hit>());
    }

    private void OnStunEnded()
    {
        StateMachine stateMachine = GetComponent<StateMachine>();
        stateMachine.SetState<DroneState>(StunPreviousState);
    }

}
