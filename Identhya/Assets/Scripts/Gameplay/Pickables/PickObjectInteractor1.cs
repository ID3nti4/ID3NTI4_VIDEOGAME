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
        gameObject.transform.Find("CapsuleMain").GetComponent<Renderer>().materials[0].shader = Shader.Find("Standard");
        gameObject.transform.Find("CapsuleUpperDoor").GetComponent<Renderer>().materials[0].shader = Shader.Find("Standard");
        gameObject.transform.Find("Wheels").GetComponent<Renderer>().materials[0].shader = Shader.Find("Standard");
    }
}
