using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class DogState : State
{
    protected Dog dog;
    protected NavMeshAgent navmeshAgent;

    [SerializeField] string MovementSpeedAnimKey = "MovementSpeed";

    [SerializeField] float SpeedMultiplicator = 0.3f;

    new void Awake()
    {
        base.Awake();
        dog = GetComponent<Dog>();
        machine = GetComponent<StateMachine>();
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter(State prevState)
    {
        
    }

    public override void UpdateState()
    {
        dog.animator.SetFloat(MovementSpeedAnimKey, navmeshAgent.velocity.magnitude * SpeedMultiplicator * dog.SpeedAnimationMultiplier);
    }

    public override void OnStateLeave(State nextState)
    {
        
    }
}
