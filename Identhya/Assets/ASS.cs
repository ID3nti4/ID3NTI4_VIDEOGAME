using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASS : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextDialogue dialogueToRun;

    bool DialogueStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (DialogueStarted) return;
        DialogueStarted = true;
        FindObjectOfType<DialogueRunner>().DoDialogue(dialogueToRun);
    }


}
