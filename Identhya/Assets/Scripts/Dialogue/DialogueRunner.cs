using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRunner : MonoBehaviour
{

    public TextDialogue textDialogue;

    public GameObject DialogueCanvasPrefab;

    UIDialogueManager uiDialogueManager;

    public delegate void OnNewLineAvailableDelegate(string talker, int line);
    public OnNewLineAvailableDelegate OnNewLineAvailable;

    bool Cancel = false;

    public Coroutine DoDialogue(TextDialogue whichDialogue)
    {
        textDialogue = whichDialogue;
        return DoDialogue();
    }

    public Coroutine DoDialogue()
    {
        uiDialogueManager = FindObjectOfType<UIDialogueManager>();
        if(uiDialogueManager == null)
        {
            Instantiate(DialogueCanvasPrefab);
            uiDialogueManager = FindObjectOfType<UIDialogueManager>();
        }

        textDialogue.Reset();

        return StartCoroutine(RunDialogue());


    }

    int AskResponse = -1;

    IEnumerator RunDialogue()
    {
        int line = 1;
        uiDialogueManager.FadeToOpacityImmediately(0.0f);
        DialogueItem nextItem = textDialogue.GetNextItem();
        yield return new WaitForEndOfFrame();
        while(nextItem != null)
        {
            if (nextItem.ask.items.Length == 0)
            {
                string nextLine = nextItem.line;
                Debug.Log("About to invoke line: " + nextLine);
                OnNewLineAvailable?.Invoke(nextLine, line);
                float duration = nextLine.Length * 0.06f;
                uiDialogueManager.ShowLine(nextLine);
                yield return uiDialogueManager.FadeToOpaque();
                yield return StartCoroutine(CancellableDelay(duration));
                yield return uiDialogueManager.FadeToTransparent();
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                OnNewLineAvailable?.Invoke("Kira:", line);
                string AskX = "";
                string AskY = "";
                string AskB = "";
                if(nextItem.ask.items.Length == 2)
                {
                    AskX = nextItem.ask.items[0].option;
                    AskB = nextItem.ask.items[1].option;
                    uiDialogueManager.ShowAsks(AskX, AskB);
                }
                if(nextItem.ask.items.Length == 3)
                {
                    AskX = nextItem.ask.items[0].option;
                    AskB = nextItem.ask.items[1].option;
                    AskY = nextItem.ask.items[2].option;
                    uiDialogueManager.ShowAsks(AskX, AskB, AskY);
                }
                yield return StartCoroutine(GetResponse(nextItem.ask.items.Length));
                uiDialogueManager.HideAsks();
                yield return new WaitForSeconds(0.25f);
                textDialogue = nextItem.ask.items[AskResponse].next;
                Debug.Log("Selected response: " + AskResponse + "; first line: " + textDialogue.items[0].line);
                textDialogue.Reset();
                
            }
            if(nextItem.action_N != null)
            {
                yield return nextItem.action_N.DoAction(this.gameObject);
            }
            ++line;
            
            nextItem = textDialogue.GetNextItem();
            if(nextItem == null && textDialogue.next_N != null)
            {
                textDialogue = textDialogue.next_N;
                textDialogue.Reset();
                nextItem = textDialogue.GetNextItem();
            }
        }
        Debug.Log("newItem was null!");
        OnNewLineAvailable?.Invoke("", line+1);

    }

    IEnumerator GetResponse(int n)
    {
        AskResponse = -1;
        while(AskResponse == -1)
        {
            if(Input.GetButton("X"))
            {
                AskResponse = 0;
            }
            if (Input.GetButton("B"))
            {
                AskResponse = 1;
            }
            if (Input.GetButton("Y") && n > 2)
            {
                AskResponse = 2;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CancellableDelay(float Seconds)
    {
        float remaining = Seconds;
        while(remaining > 0.0f && (!Cancel))
        {
            remaining -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Cancel = false;
    }

}
