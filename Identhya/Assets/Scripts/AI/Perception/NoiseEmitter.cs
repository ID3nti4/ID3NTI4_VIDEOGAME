using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseEmitter : MonoBehaviour
{
    [SerializeField] float Strength;

    List<Hearing> ListenersInRange;

    Vector3 LastLocation;

    public void RegisterListener(Hearing listener)
    {
        ListenersInRange.Add(listener);
    }

    public void UnregisterListener(Hearing listener)
    {
        ListenersInRange.Remove(listener);
    }

    private void Awake()
    {
        ListenersInRange = new List<Hearing>();
    }

    public void EmitNoise()
    {
        foreach(Hearing component in ListenersInRange)
        {
            if(component != null)
            component.dbSignal(Strength, this.gameObject);
        }
    }

}
