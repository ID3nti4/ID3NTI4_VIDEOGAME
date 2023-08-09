using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthComponent : MonoBehaviour
{
    public bool NeedDistance = true;
    public float InitialHealth = 10.0f;
    public float CurrentHealth;
    public float gradualDecreaseMultiplier;
    public float redLightCounter;

    public delegate void OnCharacterDiedDelegate(GameObject Source);
    public delegate void OnCharacterDamageDelegate(float Amount, AttackModifier Modifier, GameObject Source);

    public OnCharacterDiedDelegate OnCharacterDied;
    public OnCharacterDiedDelegate OnCharacterBlockedDamage;
    public OnCharacterDamageDelegate OnCharacterDamage;




    protected void Start()
    {
        CurrentHealth = InitialHealth;
    }


    public virtual float CalculateDamage(float Damage, AttackModifier modifier)
    {
        return Damage;
    }

    public void TakeDamage(float Damage, AttackModifier Modifier, GameObject Source = null)
    {
        //Debug.Log("Damage taken from " + ((Source != null) ? Source.name : "nobody"));
        if (CurrentHealth <= 0.0f) return;

        float DamageToApply = CalculateDamage(Damage, Modifier);

        if (DamageToApply == 0.0f)
        {
            OnCharacterBlockedDamage?.Invoke(Source);
            return;
        }

        CurrentHealth = Mathf.Max(0, CurrentHealth - Damage);
        if (CurrentHealth <= 0.0f)
        {
            OnCharacterDied?.Invoke(Source);
        }
        else
        {
            OnCharacterDamage?.Invoke(Damage, Modifier, Source);
        }
    }

    public void TakeDamage(float Damage, GameObject Source = null)
    {
        TakeDamage(Damage, null, Source);
    }

    public void RecoverHealth(float Heal)
    {
        CurrentHealth = Mathf.Min(InitialHealth, CurrentHealth + Heal);
    }

    public IEnumerator GradualHealthDecrease()
    {
        Debug.Log("Entra a GradualHealthDecrease");
        /*if(redLightCounter > 0)
        {
            Debug.Log("Entra al IF");
            CurrentHealth -= gradualDecreaseMultiplier * Time.deltaTime;
            yield return null;
        }*/
        while(redLightCounter > 0)
        {
            CurrentHealth -= gradualDecreaseMultiplier * Time.deltaTime;
            if (CurrentHealth <= 0.05f)
            {
                TakeDamage(1f);
            }
            yield return null;
        }
    }

    public void IncreaseRedLightCounter(float amount)
    {
        Debug.Log("Se aumenta el contador de luces rojas");
        redLightCounter += amount;
        redLightCounter = Mathf.Clamp(redLightCounter, 0f, 10f);
    }

    public void DecreaseRedLightCounter(float amount)
    {
        Debug.Log("Se reduce el contador de luces rojas");
        redLightCounter -= amount;
        redLightCounter = Mathf.Clamp(redLightCounter, 0f, 10f);
    }
}
