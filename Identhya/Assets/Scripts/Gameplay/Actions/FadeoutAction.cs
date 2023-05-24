using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeoutAction : GameplayAction
{
    public UIFader fader;

    public override Coroutine DoAction(GameObject source)
    {
        if (fader != null)
            return fader.FadeToOpaque();

        else
            return StartCoroutine(Delay(5.0f));
    }

    IEnumerator Delay(float s)
    {
        yield return new WaitForSeconds(s);
    }
}
