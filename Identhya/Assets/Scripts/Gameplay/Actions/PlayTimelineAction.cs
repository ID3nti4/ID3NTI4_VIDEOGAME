using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayTimelineAction : GameplayAction
{
    
    public PlayableDirector timeline_A;

    bool isDone = false;

    public override Coroutine DoAction(GameObject source)
    {
        if (timeline_A == null) timeline_A = GetComponent<PlayableDirector>();
        isDone = false;
        
        timeline_A.Play();
        timeline_A.played += (p) => { isDone = true; };

        return StartCoroutine(WaitUntilPlayed());
    }

    IEnumerator WaitUntilPlayed()
    {
        while(!isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
}
