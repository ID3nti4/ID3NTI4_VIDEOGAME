using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectPlayerAction : GameplayAction
{
    public override Coroutine DoAction(GameObject source)
    {
        Kira kira = FindObjectOfType<Kira>();
        kira.SetPlayerBlocked(true);
        kira.DisablePhysics();
        kira.GetComponentInChildren<Animator>().applyRootMotion = false;
        return StartCoroutine(FinishImmediately());
    }
}
