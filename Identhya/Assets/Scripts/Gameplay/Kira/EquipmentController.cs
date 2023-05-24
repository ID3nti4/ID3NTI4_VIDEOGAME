using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public GameObject BootsGO;
    public GameObject GlovesGO;

    private void Start()
    {
        if(FindObjectOfType<InventoryController>() == null)
        {
            GameObject newGO = new GameObject();
            newGO.AddComponent<InventoryController>();
            newGO.name = "InventoryController (dynamic spawn)";
        }
        if (FindObjectOfType<InventoryController>().HasBoots()) GetBoots();
        else BootsGO.SetActive(false);
        if (FindObjectOfType<InventoryController>().HasGloves()) GetGloves();
        else GlovesGO.SetActive(false);
    }

    public void GetBoots()
    {
        BootsGO.SetActive(true);
    }

    public void GetGloves()
    {
        GlovesGO.SetActive(true);
    }
}
