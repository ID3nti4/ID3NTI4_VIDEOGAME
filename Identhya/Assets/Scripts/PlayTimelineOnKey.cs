using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayTimelineOnKey : MonoBehaviour
{
    public bool Force = false;
    public PlayableDirector timeline_A;
    public string Key = "CavernTimelinePlayed";
    public GameplayAction CantPlayAction_N;
    // Start is called before the first frame update
    void Awake()
    {
        if(timeline_A == null) timeline_A = GetComponent<PlayableDirector>();
        if(PlayerPrefs.GetInt(Key) == 0 || Force)
        {
            timeline_A.Play();
            PlayerPrefs.SetInt(Key, 1);
        }
        else
        {
            if(CantPlayAction_N != null)
            {
                CantPlayAction_N.DoAction(this.gameObject);
            }
        }
    }
}
