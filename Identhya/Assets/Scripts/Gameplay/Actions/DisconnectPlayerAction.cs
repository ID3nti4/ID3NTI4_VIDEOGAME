using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectPlayerAction : GameplayAction
{
    public override Coroutine DoAction(GameObject source)
    {
        Debug.Log("empìeza Element3");
        Kira kira = FindObjectOfType<Kira>();
        kira.SetPlayerBlocked(true);
        kira.DisablePhysics();
        kira.GetComponentInChildren<Animator>().applyRootMotion = false;
        Debug.Log("termina Element3");
        return StartCoroutine(FinishImmediately());
    }
}
