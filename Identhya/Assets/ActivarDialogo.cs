using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarDialogo : MonoBehaviour
{
    [SerializeField] TextDialogue dialogueToRun;
    
    bool DialogueStarted = false;
    private void OnTriggerEnter(Collider other)
    {
        if (DialogueStarted) return;
        DialogueStarted = true;
        FindObjectOfType<DialogueRunner>().DoDialogue(dialogueToRun);
    }
}
