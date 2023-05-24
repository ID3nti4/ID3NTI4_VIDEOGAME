using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioClip startClip_N;
    [SerializeField] AudioCrossfader xfader_A;

    int currentClip = 0;

    AudioClip playingClip = null;

    void Awake()
    {
        xfader_A = GetComponent<AudioCrossfader>();
        if(startClip_N!=null)
        {
            Play(startClip_N);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Play(AudioClip clip, bool loop = true)
    {
        if(clip == null)
        {
            Stop();
        }
        playingClip = clip;
        xfader_A.Play(clip, loop);
    }

    public AudioClip GetPlayingClip()
    {
        return playingClip;
    }

    public void Play(int index, bool loop = true)
    {
        Play(clips[index], loop);
    }

    public void Play(string clipName, bool loop = true)
    {
        foreach(AudioClip clip in clips)
        {
            if(clip.name == clipName)
            {
                Play(clip, loop);
                return;
            }
        }
        Stop();
    }

    public void Stop()
    {
        xfader_A.Stop();
        playingClip = null;
    }
}
