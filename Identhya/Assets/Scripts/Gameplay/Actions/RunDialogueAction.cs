using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDialogueAction : GameplayAction
{
    public TextDialogue textDialogue;

    public bool JustOnce = false;

    bool done = false;

    public void StartAction(GameObject source)
    {
        StartCoroutine(DoActionCoRo(source));
    }

    IEnumerator DoActionCoRo(GameObject source)
    {
        yield return DoAction(source);
    }

    public override Coroutine DoAction(GameObject source)
    {
       if(JustOnce && done)
       {
            return StartCoroutine(FinishImmediately());
       }

        else
        {
            DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
            return dialogueRunner.DoDialogue(textDialogue);
        }
    }
}
