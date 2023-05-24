using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : MonoBehaviour
{
    [SerializeField] float dbThreshold = 1.0f;

    public Sight.OnObjectDetectStateDelegate OnObjectHeard;

    public void dbSignal(float Strength, GameObject source)
    {
        float Distance = (source.transform.position - this.transform.position).magnitude;
        float effectiveStrength = Strength / Distance;
        if (effectiveStrength > dbThreshold)
        {
            OnObjectHeard?.Invoke(source, this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            NoiseEmitter emitter = other.gameObject.GetComponent<NoiseEmitter>();
            if(emitter != null)
            {
                emitter.RegisterListener(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            NoiseEmitter emitter = other.gameObject.GetComponent<NoiseEmitter>();
            if (emitter != null)
            {
                emitter.UnregisterListener(this);
            }
        }
    }
}
