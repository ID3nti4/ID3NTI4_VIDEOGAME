using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFHC_Shader_Samples;

public class PickObjectInteractor1 : Interactor
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
        gameObject.GetComponent<Renderer>().materials[1].shader = Shader.Find("Standard");
    }
}
