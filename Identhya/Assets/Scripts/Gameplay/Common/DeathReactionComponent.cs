using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathReactionComponent : MonoBehaviour
{
    public string NormalDeathAnimKey = "Death Back";
    public string KillVolumeAnimKey = "Death Front";
    public Animator animator;

    private void Start()
    {
        HealthComponent healthComponent = gameObject.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.OnCharacterDied += OnDeath;
        }
        animator = GetComponentInChildren<Animator>();
    }

    private void OnDeath(GameObject Source)
    {
        
        if(Source != null && Source.GetComponent<KillVolume>()!=null)
        {
            animator.SetTrigger(KillVolumeAnimKey);
        }
        else
        {
            animator.SetTrigger(NormalDeathAnimKey);
        }
        
    }
}
