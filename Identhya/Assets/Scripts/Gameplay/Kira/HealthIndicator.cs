using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class HealthIndicator : MonoBehaviour
{

    public Image healthIndicator;
    public GameObject kira;
    public HealthComponent kiraHealthComponent;

    private void Start()
    {
        kiraHealthComponent = kira.GetComponent<HealthComponent>();
    }


    void Update()
    {
        healthIndicator.fillAmount = kiraHealthComponent.CurrentHealth / kiraHealthComponent.InitialHealth;
    }
}
