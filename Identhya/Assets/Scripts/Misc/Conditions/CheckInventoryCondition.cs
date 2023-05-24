using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventoryCondition : Condition
{
    public InventoryController.InventoryItems item;

    public override bool CheckCondition(GameObject source)
    {
        InventoryController controller = FindObjectOfType<InventoryController>();
        if(controller == null)
        {
            return false;
        }

        return controller.HasItem(item);
    }
}
