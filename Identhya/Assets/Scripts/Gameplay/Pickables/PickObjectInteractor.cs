using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjectInteractor : Interactor
{
    public InventoryController.InventoryItems Item;

    public GameplayAction action_N;

    public override void Interact()
    {
        FindObjectOfType<InventoryController>().GetItem(Item);
        if(action_N != null)
        {
            action_N.DoAction(this.gameObject);
        }
        Destroy(this.gameObject, 0.08f);
    }
}
