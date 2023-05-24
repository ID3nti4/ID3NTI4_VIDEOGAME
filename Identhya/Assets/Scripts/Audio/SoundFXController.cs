using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioCrossfader))]
public class SoundFXController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioCrossfader xfader_A;

    int currentClip = 0;

    void Awake()
    {
        xfader_A = GetComponent<AudioCrossfader>();    
    }

    public void Play(AudioClip clip, bool loop)
    {
        xfader_A.Play(clip, loop);
    }

    public void Stop()
    {
        xfader_A.Stop();
    }
}
