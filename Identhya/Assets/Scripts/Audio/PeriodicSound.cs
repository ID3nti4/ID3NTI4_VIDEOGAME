using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PeriodicSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip clip;
    public float MinInterval = 5.0f;
    public float MaxInterval = 30.0f;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();
        while (1<2)
        {
            float interval = Random.Range(MinInterval, MaxInterval);
            yield return new WaitForSeconds(interval);
            PlaySound(clip);
        }
    }

    void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);
            audioSource.pitch = randomPitch;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
