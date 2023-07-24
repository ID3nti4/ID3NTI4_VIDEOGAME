using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : Interactor
{
    public NPCController npc;

    bool HasInteracted = false;

    public override void Interact()
    {
        if (HasInteracted) return;

        HasInteracted = true;

        StartCoroutine(ScaleIconDown());
        interactingComponent.gameObject.GetComponent<Kira>().SetPlayerBlocked(true);
        FindObjectOfType<DialogueRunner>().OnNewLineAvailable += OnNewLine;
    }

    private void OnNewLine(string line, int lineN)
    {
        Debug.Log("New Line: " + line + ", " + lineN);
        if(line == "")
        {
            FindObjectOfType<DialogueRunner>().OnNewLineAvailable -= OnNewLine;
            npc.SetSpeaking(false);
            Destroy(this.gameObject);
            interactingComponent.gameObject.GetComponent<Kira>().SetPlayerBlocked(false);
            return;
        }
        npc.SetSpeaking(!line.StartsWith("Kir4"));
        npc.SetAttitude(lineN%4);
    }
}
