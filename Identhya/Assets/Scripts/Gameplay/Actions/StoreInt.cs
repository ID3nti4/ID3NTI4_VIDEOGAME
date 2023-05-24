using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInt : GameplayAction
{
    public string Key;
    public int Value;
    public override Coroutine DoAction(GameObject source)
    {
        PlayerPrefs.SetInt(Key, Value);
        return StartCoroutine(FinishImmediately());
    }
}
