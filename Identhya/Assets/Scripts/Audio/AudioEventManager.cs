using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventManager : MonoBehaviour
{
    AudioSource localAudioSource;
    SFXController sfxController;
    // Start is called before the first frame update
    void Start()
    {
        localAudioSource = GetComponentInChildren<AudioSource>();
        if (localAudioSource == null)
        {
            localAudioSource = GetComponentInParent<AudioSource>();
        }
        sfxController = FindObjectOfType<SFXController>();
    }

    public void PlaySound(Object obj)
    {
        AudioClip clip = (AudioClip)(obj);
        if (localAudioSource != null)
        {
            localAudioSource.PlayOneShot(clip);
        }
        else
        {
            if (sfxController != null)
            {
                sfxController.PlaySound(clip);
            }
        }
    }
}
