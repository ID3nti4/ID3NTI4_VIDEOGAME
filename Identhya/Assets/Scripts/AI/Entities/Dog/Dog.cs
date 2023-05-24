using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : AnimatedCharacter
{
    public float SearchSpeed = 1.0f;
    public float WalkSpeed = 2.0f;
    public float PeltSpeed = 6.0f;
    public float AttackSpeed = 10.0f;
    public float SpeedAnimationMultiplier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.OnCharacterDied += OnDogDied;
        }

    }

    private void OnDogDied(GameObject Source)
    {
        StateMachine stateMachine = GetComponent<StateMachine>();
        if(stateMachine != null)
        {
            Debug.Log(" ====================================================================== DEFEATING  ");
            stateMachine.SetState(GetComponent<DogState_Defeated>());
        }
    }

}
