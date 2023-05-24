using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventoryAtEnd : GameplayAction
{
    public override Coroutine DoAction(GameObject source)
    {
        InventoryController inventory = FindObjectOfType<InventoryController>();

        if (!inventory.HasItem(InventoryController.InventoryItems.Boomerang))
        {
            FindObjectOfType<BoomerangController>().SpawnBoomerang();
        }
        return StartCoroutine(FinishImmediately());
    }
}
