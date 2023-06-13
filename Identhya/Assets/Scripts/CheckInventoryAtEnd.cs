using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventoryAtEnd : GameplayAction
{
    public override Coroutine DoAction(GameObject source)
    {
        Debug.Log("empieza Element2");
        InventoryController inventory = FindObjectOfType<InventoryController>();

        if (!inventory.HasItem(InventoryController.InventoryItems.Boomerang))
        {
            FindObjectOfType<BoomerangController>().SpawnBoomerang();
        }

        Debug.Log(inventory.HasItem(InventoryController.InventoryItems.Boomerang));
        Debug.Log("termina Element2");
        return StartCoroutine(FinishImmediately());
    }
}
