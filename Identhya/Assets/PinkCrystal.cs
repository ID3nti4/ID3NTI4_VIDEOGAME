using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkCrystal : MonoBehaviour
{
    private HealthComponent playerHealth;
    public GameObject Prefab;
    public AudioClip clip;
    public Transform transform1;
    private bool done;

    private void Start()
    {
        playerHealth = GameObject.Find("Kira_V2").GetComponent<HealthComponent>();
    }

    public void DestroyCrystal()
    {
        if (!done)
        {
            playerHealth.RecoverHealth(3f);
            gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
            gameObject.transform.Find("SM_Env_Gem_Large_02_new").GetComponent<Renderer>().materials[1].shader = Shader.Find("Standard");
            done = true;
        }
    }
}
