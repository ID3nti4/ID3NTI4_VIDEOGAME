using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrystal : MonoBehaviour
{
    private EnergySystem playerEnergy;
    public GameObject Prefab;
    public Transform transform1;
    private bool done;

    private void Start()
    {
        playerEnergy = GameObject.Find("KiraV2").GetComponent<EnergySystem>();
    }

    public void DestroyCrystal()
    {
        if (!done)
        {
            playerEnergy.IncreaseMaxEnergy(10f);
            gameObject.transform.Find("SM_Env_Gem_Large_02_new").GetComponent<Renderer>().materials[1].shader = Shader.Find("Standard");
            done = true;
        }
    }
}
