using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyConsumption : MonoBehaviour
{
    public bool redLight = false;

    private void OnTriggerEnter(Collider other)
    {
        if(redLight == true && other.gameObject.tag == "Player")
        {
            other.GetComponent<EnergySystem>().redLightHitting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<EnergySystem>().redLightHitting = false;
        }
    }
}
