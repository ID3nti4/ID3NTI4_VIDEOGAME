using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    public GameObject ThingToDestroy;
    private void Start()
    {
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.OnCharacterDied += OnDeath;
        }

    }

    private void OnDeath(GameObject Source)
    {
        Destroy(ThingToDestroy);
    }

}
