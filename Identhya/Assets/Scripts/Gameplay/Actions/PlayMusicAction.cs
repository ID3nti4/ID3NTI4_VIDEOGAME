using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicAction : GameplayAction
{
    public AudioClip track;
    public string trackName;

    MusicController musicController;

    private void Start()
    {
        musicController = FindObjectOfType<MusicController>();
    }

    public override Coroutine DoAction(GameObject source)
    {
        if(musicController != null)
        {
            if(track != null)
            {
                musicController.Play(track);
            }
            else
            {
                musicController.Play(trackName);
            }
        }
        return StartCoroutine(FinishImmediately());
    }

}
