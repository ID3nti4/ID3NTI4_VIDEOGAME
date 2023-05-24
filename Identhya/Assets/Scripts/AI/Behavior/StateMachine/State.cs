using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public string StateName;
    
    public string StateClass;

    protected StateMachine machine;
    protected void Awake()
    {
        machine = GetComponent<StateMachine>();
        if(machine == null)
        {
            Debug.Log("Machine = null ??");
        }
    }
    public abstract void UpdateState();
    public abstract void OnStateEnter(State prevState);
    public abstract void OnStateLeave(State nextState);

}
