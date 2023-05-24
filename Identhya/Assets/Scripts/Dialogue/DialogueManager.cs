using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Interactor npcInteractor;

    DialogueRunner dialogueRunner;

    public TextDialogue noFoodDialogue;
    public TextDialogue foodDialogue;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        npcInteractor.OnObjectInteracted += OnNPCInteracted;
    }

    private void OnNPCInteracted()
    {
        StartCoroutine(NPCInteractedCoRo());        
    }

    IEnumerator NPCInteractedCoRo()
    {
        InventoryController inventory = FindObjectOfType<InventoryController>();
        if (inventory != null && inventory.HasItem(InventoryController.InventoryItems.FoodCan))
        {
            yield return dialogueRunner.DoDialogue(foodDialogue);
        }
        else
        {
            yield return dialogueRunner.DoDialogue(noFoodDialogue);
        }
    }
}
