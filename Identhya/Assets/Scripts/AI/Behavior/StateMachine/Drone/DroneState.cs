using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneState : State
{
    protected Drone drone;
    protected NavMeshAgent navmeshAgent;
    protected new void Awake()
    {
        base.Awake();
        drone = GetComponent<Drone>();
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter(State prevState)
    {

    }

    public override void UpdateState()
    {

    }

    public override void OnStateLeave(State nextState)
    {

    }
}
