using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrystal : MonoBehaviour
{
    private EnergySystem playerEnergy;

    private void Start()
    {
        playerEnergy = GameObject.Find("KiraV2").GetComponent<EnergySystem>();
    }

    public void DestroyCrystal()
    {
        playerEnergy.IncreaseMaxEnergy(10f);
        Destroy(gameObject);
    }
}
