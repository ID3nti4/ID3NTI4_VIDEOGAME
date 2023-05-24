using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioCrossfader : MonoBehaviour
{
    [SerializeField] int NumberOfAudioSources = 2;
    [SerializeField] bool _3DSound;

    AudioSource[] audioSources;
    float[] targetVolume;
    float[] volume;

    [SerializeField] float FadeTime = 1.0f;

    [SerializeField] float MasterVolume = 0.4f;
    [SerializeField] AudioMixer audioMixer;

    AudioSource nextSource;
    AudioSource prevSource;
    int nextIndex = 0;

    void Awake()
    {
        audioSources = new AudioSource[NumberOfAudioSources];
        for(int i = 0; i < NumberOfAudioSources; ++i)
        {
            audioSources[i] = this.gameObject.AddComponent<AudioSource>();
            audioSources[i].outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
            audioSources[i].spatialBlend = _3DSound ? 1.0f : 0.0f;
        }
        prevSource = audioSources[0];
        nextSource = audioSources[1];
        nextIndex = 1;
    }

    public void Play(AudioClip clip, bool loop = false)
    {
        Debug.Log("xFader playing: " + clip.name);
        StartCoroutine(CrossFade(clip, prevSource, nextSource, loop));
        prevSource = audioSources[nextIndex];
        nextIndex = (nextIndex + 1) % NumberOfAudioSources;
        nextSource = audioSources[nextIndex];
    }

    public void Stop()
    {
        Play(null, false);
    }
    
    IEnumerator CrossFade(AudioClip clip, AudioSource prev, AudioSource next, bool loop)
    {
        nextSource.clip = clip;
        float t = 0.0f;
        while(t < 1.0f)
        {
            t += Time.deltaTime * (1.0f / FadeTime);
            if(t > 0 && !next.isPlaying)
            {
                next.loop = loop;
                next.Play();
            }
            next.volume = t * MasterVolume;
            prev.volume = (1.0f - t) * MasterVolume;
            yield return new WaitForEndOfFrame();
        }
        next.volume = MasterVolume;
        prev.volume = 0.0f;
        prev.Stop();
    }

}
