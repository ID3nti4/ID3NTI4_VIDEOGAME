using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXEventsManager : MonoBehaviour
{
    public AudioClip[] footsteps;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FootL()
    {
        //Debug.Log("FOOT L");
        Footstep();
    }

    public void FootR()
    {
        //Debug.Log("FOOT R");
        Footstep();
    }

    public void Shoot()
    {

    }
    private void Footstep()
    {
        int index = Random.Range(0, footsteps.Length);
        audioSource.PlayOneShot(footsteps[index]);
        GetComponent<NoiseEmitter>().EmitNoise();
    }

}
