using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    State CurrentState;
    [SerializeField] State _CheckState;

    public delegate void OnStateChangedDelegate(string StateName, string StateClass);

    public OnStateChangedDelegate OnStateChanged;

    void Start()
    {
        CurrentState = GetComponent<StartState>();
        CurrentState.OnStateEnter(null);
        if(CurrentState == null)
        {
            Debug.Log("   Warning: no start state specified ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateState();
        _CheckState = CurrentState;
    }

    public State GetState()
    {
        return CurrentState;
    }

    public void SetState(State nextState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateLeave(nextState);
        }
        nextState.OnStateEnter(CurrentState);
        CurrentState = nextState;
        OnStateChanged?.Invoke(CurrentState.StateName, CurrentState.StateClass);
    }

    public void SetState<T>(T nextState) where T:State
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateLeave(nextState);
        }
        nextState.OnStateEnter(CurrentState);
        CurrentState = nextState;
        OnStateChanged?.Invoke(CurrentState.StateName, CurrentState.StateClass);
    }
}
