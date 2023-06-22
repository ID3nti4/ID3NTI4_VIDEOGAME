using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class HealthIndicator : MonoBehaviour
{

    public Image healthIndicator;
    HealthComponent healthComponent;

  
    void Update()
    {
        healthIndicator.fillAmount = healthComponent.CurrentHealth / healthComponent.InitialHealth;
    }
}
