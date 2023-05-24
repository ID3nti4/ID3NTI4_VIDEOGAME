using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAction : GameplayAction
{
    public AudioClip clip;
    AudioSource localAudioSource;
    SFXController sfxController;
    // Start is called before the first frame update
    void Start()
    {
        localAudioSource = GetComponentInChildren<AudioSource>();
        if(localAudioSource == null)
        {
            localAudioSource = GetComponentInParent<AudioSource>();
        }
        sfxController = FindObjectOfType<SFXController>();
    }

    public override Coroutine DoAction(GameObject source)
    {
        if(localAudioSource != null)
        {
            localAudioSource.PlayOneShot(clip);
        }
        else
        {
            if(sfxController != null)
            {
                sfxController.PlaySound(clip);
            }
        }
        return StartCoroutine(FinishImmediately());
    }

}
