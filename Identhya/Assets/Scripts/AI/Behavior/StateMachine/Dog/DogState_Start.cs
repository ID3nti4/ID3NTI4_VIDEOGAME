using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState_Start : DogStartState
{
    public override void UpdateState()
    {
     
    }

    public override void OnStateEnter(State prevState)
    {
        machine.SetState(GetComponent<DogState_Idle>());   
    }

    public override void OnStateLeave(State nextState)
    {
        
    }
}
