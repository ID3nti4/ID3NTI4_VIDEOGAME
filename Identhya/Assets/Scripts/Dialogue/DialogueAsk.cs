using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AskItem
{
    public string option;
    public TextDialogue next;
}

[System.Serializable]
public class DialogueAsk
{
    public AskItem[] items;
}
