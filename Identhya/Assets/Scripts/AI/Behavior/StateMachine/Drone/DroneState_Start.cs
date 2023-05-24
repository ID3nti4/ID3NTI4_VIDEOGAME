using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneState_Start : DroneStartState
{
    public override void OnStateLeave(State nextState)
    {
        
    }

    public override void UpdateState()
    {
        
    }

    public override void OnStateEnter(State prevState)
    {
        machine.SetState(GetComponent<DroneState_Patrol>());
    }

}
