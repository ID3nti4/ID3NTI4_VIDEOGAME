using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public float currentEnergy, recoveryMultiplier, gradualDecreaseMultiplier;
    public static float maxEnergy = 30;
    private Image energyBar;

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
        if(currentEnergy < maxEnergy)
        {
            RecoverEnergy();
        }

        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
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
    }

    public void GradualEnergyDecrease()
    {
        currentEnergy -= gradualDecreaseMultiplier + Time.deltaTime;
        UpdateEnergyUI();
    }

    private void UpdateEnergyUI()
    {
        energyBar.fillAmount = currentEnergy / 100;
    }
}
