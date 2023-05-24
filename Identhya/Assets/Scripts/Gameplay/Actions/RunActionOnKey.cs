using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionOnKey : MonoBehaviour
{
    public string Key;
    public int Value;
    public GameplayAction Action;
    private void Awake()
    {
        if(PlayerPrefs.GetInt(Key) == Value)
        {
            Action.DoAction(this.gameObject);
        }
    }
}
