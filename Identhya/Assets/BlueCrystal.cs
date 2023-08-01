using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrystal : MonoBehaviour
{
    private EnergySystem playerEnergy;
    public GameObject Prefab;
    public AudioClip clip;
    public Transform transform1;
    private bool done;

    private void Start()
    {
        playerEnergy = GameObject.Find("Kira_V2").GetComponent<EnergySystem>();
    }

    public void DestroyCrystal()
    {
        if (!done)
        {
            playerEnergy.IncreaseMaxEnergy(10f);
            gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
            gameObject.transform.Find("SM_Env_Gem_Large_02_new").GetComponent<Renderer>().materials[1].shader = Shader.Find("Standard");
            done = true;
        }
    }
}
