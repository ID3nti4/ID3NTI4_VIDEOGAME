﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public float currentEnergy, recoveryMultiplier, gradualDecreaseMultiplier;
    public static float maxEnergy = 30;
    private Image energyBar;
    public bool climbing = false, pause = false;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar = GameObject.Find("EnergyIndicator").GetComponent<Image>();
        UpdateEnergyUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnergy < maxEnergy && pause == false)
        {
            RecoverEnergy();
        }

        if(climbing == true)
        {
            GradualEnergyDecrease();
        }

        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    private IEnumerator StartPause()
    {
        pause = true;
        yield return new WaitForSeconds(1f);
        pause = false;
    }

    private void RecoverEnergy()
    {
        currentEnergy += recoveryMultiplier * Time.deltaTime;
        UpdateEnergyUI();
    }

    public void IncreaseMaxEnergy(float value)
    {
        maxEnergy += value;
        UpdateEnergyUI();
    }

    public void DecreaseEnergy(float value)
    {
        currentEnergy -= value;
        UpdateEnergyUI();
        StartCoroutine(StartPause());
    }

    public void GradualEnergyDecrease()
    {
        currentEnergy -= gradualDecreaseMultiplier * Time.deltaTime;
        UpdateEnergyUI();
        StartCoroutine(StartPause());
    }

    private void UpdateEnergyUI()
    {
        energyBar.fillAmount = currentEnergy / 100;
    }
}
