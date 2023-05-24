using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        HealthComponent healthComponent = other.gameObject.GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.TakeDamage(healthComponent.InitialHealth, this.gameObject);
        }
    }
}
