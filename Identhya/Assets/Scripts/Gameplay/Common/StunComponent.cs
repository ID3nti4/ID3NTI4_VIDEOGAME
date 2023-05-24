using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunComponent : MonoBehaviour
{
    public delegate void OnStunDelegate();
    public OnStunDelegate OnStunStarted;
    public OnStunDelegate OnStunEnded;

    public void Stun()
    {
        Debug.Log("============================== Enemy stunned! ================================= ");
        OnStunStarted?.Invoke();
    }
}
